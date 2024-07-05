using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieEditor.Encoder
{
    public interface  IEncoder
    {
        void SetMainAV(CoreAV.MainAVHandler MainAV);
        void EncodePlayPlan(CoreAV.PlayPlan Plan);
    }
}
