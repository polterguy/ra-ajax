using System;
using System.Collections.Generic;
using System.Text;

namespace Ra
{
    public interface IRaControl
    {
        void DispatchEvent(string name);
    }
}
