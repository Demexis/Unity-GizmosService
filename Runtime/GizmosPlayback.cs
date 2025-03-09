using System;

namespace GizmosService {
    public sealed class GizmosPlayback {
        public readonly string categoryName;
        public readonly Func<bool> drawCallback;
        public readonly bool permanent;
        public float timer;

        public GizmosPlayback(string categoryName, Func<bool> drawCallback) {
            this.categoryName = categoryName;
            this.drawCallback = drawCallback;
            permanent = true;
        }
        
        public GizmosPlayback(string categoryName, Func<bool> drawCallback, float timer) {
            this.categoryName = categoryName;
            this.drawCallback = drawCallback;
            this.timer = timer;
            permanent = false;
        }
    }

    public struct GizmosPlaybackInfo {
        public readonly Guid guid;
        public readonly GizmosPlayback gizmosPlayback;

        public GizmosPlaybackInfo(Guid guid, GizmosPlayback gizmosPlayback) {
            this.guid = guid;
            this.gizmosPlayback = gizmosPlayback;
        }
    }
}