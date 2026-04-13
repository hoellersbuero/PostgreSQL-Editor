using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL_Editor.Controls
{
    public partial class Expander : Component
    {
        public Expander()
        {
            InitializeComponent();
        }

        public Expander(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
