using System;
using System.Collections.Generic;
using System.Text;

namespace clacF.controllers
{
    internal interface IDimensionController
    {
        string DimensionName { get; }
        void Initialize();
        void SetActive(bool active);
        void Reset();
        void RefreshAvailability();
        bool TryExecute(out string? error);
    }
}
