using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GizmosService {
    public sealed class GizmosSystem : IGizmosSystem {
        public event Action<string> CategoryRegistered = delegate { };
        public event Action<string> CategoryUnregistered = delegate { };

        public bool ShowGizmos { get; set; }

        public readonly Dictionary<string, List<GizmosPlaybackInfo>> allRegistrations = new();
        public readonly Dictionary<string, bool> registrationsEnabled = new();

        private readonly List<GizmosPlaybackInfo> removeRegistrations = new();
        private readonly List<string> checkCategoriesCache = new();

        public GizmosSystem(bool showGizmos = true) {
            ShowGizmos = showGizmos;
        }
        
        public void Render() {
            foreach (var (category, registrations) in allRegistrations) {
                RenderCategory(category, registrations);
            }
        }

        private void RenderCategory(string categoryName, List<GizmosPlaybackInfo> registrations) {
            var categoryEnabled = registrationsEnabled[categoryName];

            foreach (var registration in registrations) {
                if (!RenderRegistration(registration, categoryEnabled)) {
                    removeRegistrations.Add(registration);
                }
            }

            if (removeRegistrations.Count == 0) {
                return;
            }
            foreach (var registration in removeRegistrations) {
                registrations.Remove(registration);
            }
            removeRegistrations.Clear();
        }

        private bool RenderRegistration(GizmosPlaybackInfo registration, bool categoryEnabled) {
            CustomGizmos.AllowDraw = ShowGizmos && categoryEnabled;
            CustomGizmos.Color = Color.white;
            return registration.gizmosPlayback.drawCallback.Invoke();
        }


        public GizmosPlaybackInfo RegisterGizmo(string categoryName, Func<bool> callback) {
            CheckCategoryExists(categoryName);
            var gizmosPlaybackInfo = new GizmosPlaybackInfo(Guid.NewGuid(), new GizmosPlayback(categoryName, callback));
            allRegistrations[categoryName].Add(gizmosPlaybackInfo);
            return gizmosPlaybackInfo;
        }

        public GizmosPlaybackInfo RegisterGizmoTemp(string categoryName, Func<bool> callback, float timer) {
            CheckCategoryExists(categoryName);
            var gizmosPlaybackInfo = new GizmosPlaybackInfo(Guid.NewGuid(), new GizmosPlayback(categoryName, callback, timer));
            allRegistrations[categoryName].Add(gizmosPlaybackInfo);
            return gizmosPlaybackInfo;
        }

        private void CheckCategoryExists(string categoryName) {
            if (allRegistrations.ContainsKey(categoryName)) {
                return;
            }
            
            allRegistrations[categoryName] = new List<GizmosPlaybackInfo>();
            registrationsEnabled.TryAdd(categoryName, true);
            CategoryRegistered.Invoke(categoryName);
        }

        public void SetCategoryEnabled(string categoryName, bool enabled) {
            registrationsEnabled[categoryName] = enabled;
        }

        private void CheckCategoryUsage(string categoryName) {
            // remove if empty
            if (allRegistrations[categoryName].Count == 0) {
                allRegistrations.Remove(categoryName);
                registrationsEnabled.Remove(categoryName);
                CategoryUnregistered.Invoke(categoryName);
            }
        }

        public void UnregisterGizmo(GizmosPlaybackInfo gizmosPlaybackInfo) {
            var categoryName = gizmosPlaybackInfo.gizmosPlayback.categoryName;
            
            if (!allRegistrations.ContainsKey(categoryName)) {
                return;
            }
            
            var categoryRegistrations = allRegistrations[categoryName];
            if (categoryRegistrations.Contains(gizmosPlaybackInfo)) {
                categoryRegistrations.Remove(gizmosPlaybackInfo);
            }
            
            CheckCategoryUsage(categoryName);
        }

        public List<string> GetAllCategories() => allRegistrations.Keys.ToList();

        // updating timers
        public void Update(float deltaTime) {
            foreach (var (categoryName, registrations) in allRegistrations) {
                foreach (var registration in registrations) {
                    if (CheckTimer(registration)) {
                        continue;
                    }
                    removeRegistrations.Add(registration);
                }

                if (removeRegistrations.Count == 0) {
                    continue;
                }
                foreach (var registration in removeRegistrations) {
                    registrations.Remove(registration);
                }
                removeRegistrations.Clear();
                checkCategoriesCache.Add(categoryName);
            }

            if (checkCategoriesCache.Count != 0) {
                foreach (var categoryName in checkCategoriesCache) {
                    CheckCategoryUsage(categoryName);
                }
                checkCategoriesCache.Clear();
            }
            
            bool CheckTimer(GizmosPlaybackInfo registration) {
                if (registration.gizmosPlayback.permanent) {
                    return true;
                }
                registration.gizmosPlayback.timer -= deltaTime;
                return registration.gizmosPlayback.timer > 0;
            }
        }
    }
}