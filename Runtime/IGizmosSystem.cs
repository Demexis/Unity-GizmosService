using System;
using System.Collections.Generic;

namespace GizmosService {
    public interface IGizmosSystem {
        /// <summary>
        /// Is called whenever new category name is registered.
        /// </summary>
        event Action<string> CategoryRegistered;
        
        /// <summary>
        /// Is called whenever category is removed.
        /// </summary>
        event Action<string> CategoryUnregistered;
        
        /// <summary>
        /// Gizmos showing status.
        /// </summary>
        bool ShowGizmos { get; }

        /// <summary>
        /// Sets active state for category. Enabled categories are rendered.
        /// Can be used for categories that are not registered.
        /// </summary>
        /// <param name="categoryName">Gizmos category.</param>
        /// <param name="enabled">Enable state.</param>
        void SetCategoryEnabled(string categoryName, bool enabled);
        
        /// <summary>
        /// Registers callback where you should use CustomGizmos class if you want to draw.
        /// Registered gizmos will render forever until:
        /// 1) you unregister them;
        /// 2) the callback returns false.
        /// </summary>
        /// <param name="categoryName">Gizmos category.</param>
        /// <param name="callback">Callback with drawing instructions.</param>
        GizmosPlaybackInfo RegisterGizmo(string categoryName, Func<bool> callback);
        
        /// <summary>
        /// Registers callback where you should use CustomGizmos class if you want to draw.
        /// Registered gizmos will render until:
        /// 1) you unregister them;
        /// 2) the callback returns false;
        /// 3) the timer expires.
        /// </summary>
        /// <param name="categoryName">Gizmos category.</param>
        /// <param name="callback">Callback with drawing instructions.</param>
        /// <param name="timer">Timer value.</param>
        GizmosPlaybackInfo RegisterGizmoTemp(string categoryName, Func<bool> callback, float timer);
        
        /// <summary>
        /// Unregisters callback.
        /// You should cache the original callback which you initially used to register gizmos,
        /// if you want to use this method.
        /// </summary>
        /// <param name="gizmosPlaybackInfo">Gizmos playback.</param>
        void UnregisterGizmo(GizmosPlaybackInfo gizmosPlaybackInfo);

        /// <summary>
        /// Get all registered categories.
        /// </summary>
        /// <returns>List of category names.</returns>
        List<string> GetAllCategories();
    }
}