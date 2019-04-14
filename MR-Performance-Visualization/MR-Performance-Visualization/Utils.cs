using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

// Create a namespace
namespace MR_Performance_Visualization
{
    /**
    * Create a class that customizes effects
    */
    class Utils
    {
        /**
        * Apply blur effect
        * @param win of type Window
        */
        public void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 5;
            win.Effect = objBlur;
        }

        /**
        * Removes blur effect
        * @param win of type Window
        */
        public void ClearEffect(Window win)
        {
            win.Effect = null;
        }
    }
}
