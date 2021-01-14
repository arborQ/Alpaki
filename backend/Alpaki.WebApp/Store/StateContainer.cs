using System;

namespace Alpaki.WebApp.Store
{
    public class StateContainer
    {
        public string BearerToken { get; private set; } = string.Empty;

        public event Action OnChange;

        public void SetToken(string value)
        {
            BearerToken = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
