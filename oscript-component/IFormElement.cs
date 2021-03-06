﻿using ScriptEngine.Machine;
using System.Windows.Forms;

namespace oscriptGUI
{
    interface IFormElement : IValue
    {
        void setParent(IValue parent);
        IValue Parent { get; }
        string Name { get; set; }
        Control getBaseControl();
        Control getControl();

        void setAction(IRuntimeContextInstance contex, string eventName, string methodName);
        string GetAction(string eventName);

        bool AutoSize { get; set; }

        int Height { get; set; }
        int Width { get; set; }

        int Dock { get; set; }
    }
}
