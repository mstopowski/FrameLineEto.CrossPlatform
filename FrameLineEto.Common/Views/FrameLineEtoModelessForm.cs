using Eto.Drawing;
using Eto.Forms;
using System;
using Rhino.Collections;
using FrameLineEto.Common.Methods;
using FrameLineEto.Common.Commands;

namespace FrameLineEto.Common.Views
{
    class FrameLineEtoModelessForm : Form
    {
        public FrameLineEtoModelessForm()
        {
            Maximizable = false;
            Minimizable = false;
            Padding = new Padding(5);
            Resizable = false;
            ShowInTaskbar = true;
            Title = GetType().Name;
            WindowStyle = WindowStyle.Default;
            RhinoList<TextBox> main_text = new RhinoList<TextBox>();
            RhinoList<TextBox> mod1_text = new RhinoList<TextBox>();
            RhinoList<TextBox> mod2_text = new RhinoList<TextBox>();
            RhinoList<TextBox> mod3_text = new RhinoList<TextBox>();
            
            #region Main Frame Line
            var from_label = new Label
            {
                Text = "From",
                TextAlignment = TextAlignment.Left,
            };

            var to_label = new Label
            {
                Text = "To",
                TextAlignment = TextAlignment.Left,
            };

            var spacing_label = new Label
            {
                Text = "Spacing",
                TextAlignment = TextAlignment.Left,
            };

            var from_text = new TextBox();
            main_text.Add(from_text);

            var to_text = new TextBox();

            var spacing_text = new TextBox();
            main_text.Add(spacing_text);

            var gruoup_main = new GroupBox
            {
                Text = "Main frame line",
                Padding = new Padding(5, 10, 5, 5),
                Content = new TableLayout
                {
                    Rows =
                    {
                        new TableRow(null, from_label, to_label, spacing_label),
                        new TableRow(null, from_text, to_text, spacing_text),
                    }
                },

            };
            #endregion

            #region Modifications
            var from_mod_label = new Label
            {
                Text = "From",
                TextAlignment = TextAlignment.Left,
            };

            var to_mod_label = new Label
            {
                Text = "To",
                TextAlignment = TextAlignment.Left,
            };

            var spacing_mod_label = new Label
            {
                Text = "Spacing",
                TextAlignment = TextAlignment.Left,
            };

            var mod_1 = new Label
            {
                Text = "#1",
                TextAlignment = TextAlignment.Center,
            };

            var mod_2 = new Label
            {
                Text = "#2",
                TextAlignment = TextAlignment.Center,
            };

            var mod_3 = new Label
            {
                Text = "#3",
                TextAlignment = TextAlignment.Center,
            };

            var mod_1_from_text = new TextBox();
            mod1_text.Add(mod_1_from_text);

            var mod_1_to_text = new TextBox();
            mod1_text.Add(mod_1_to_text);

            var mod_1_spac_text = new TextBox();
            mod1_text.Add(mod_1_spac_text);

            var mod_2_from_text = new TextBox();
            mod2_text.Add(mod_2_from_text);

            var mod_2_to_text = new TextBox();
            mod2_text.Add(mod_2_to_text);

            var mod_2_spac_text = new TextBox();
            mod2_text.Add(mod_2_spac_text);

            var mod_3_from_text = new TextBox();
            mod3_text.Add(mod_3_from_text);

            var mod_3_to_text = new TextBox();
            mod3_text.Add(mod_3_to_text);

            var mod_3_spac_text = new TextBox();
            mod3_text.Add(mod_3_spac_text);

            var group_mod = new GroupBox
            {
                Text = "Frame line modifications",
                Padding = new Padding(5, 10, 5, 5),
                Content = new TableLayout
                {
                    Rows =
                    {
                        new TableRow(null, from_mod_label, to_mod_label, spacing_mod_label),
                        new TableRow(mod_1, mod_1_from_text, mod_1_to_text, mod_1_spac_text),
                        new TableRow(mod_2, mod_2_from_text, mod_2_to_text, mod_2_spac_text),
                        new TableRow(mod_3, mod_3_from_text, mod_3_to_text, mod_3_spac_text),
                    }
                },
            };
            #endregion

            #region Buttons
            var hello_button = new Button { Text = "Create" };
            hello_button.Click += (sender, e) => CreateFrameLine(this, main_text, mod1_text, mod2_text, mod3_text);

            var close_button = new Button { Text = "Cancel" };
            close_button.Click += (sender, e) => Close();

            var button_layout = new TableLayout
            {
                Padding = new Padding(5, 10, 5, 5),
                Spacing = new Size(5, 5),
                Rows = { new TableRow(null, hello_button, null, close_button, null) }
            };

            #endregion

            Content = new TableLayout
            {
                Padding = new Padding(5),
                Spacing = new Size(5, 5),
                Rows =
                {
                    new TableRow(gruoup_main),
                    new TableRow(group_mod),
                    new TableRow(button_layout),
                }
            };
        }

        public void CreateFrameLine(FrameLineEtoModelessForm aaa, RhinoList<TextBox> main, RhinoList<TextBox> mod1, RhinoList<TextBox> mod2, RhinoList<TextBox> mod3)
        {

            
        }

        public bool CheckInputValidity(RhinoList<TextBox> list)
        {
            if (true)
            {

            }
            return true;
        }
    }
}
