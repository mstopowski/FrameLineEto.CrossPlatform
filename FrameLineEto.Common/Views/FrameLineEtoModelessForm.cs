using Eto.Drawing;
using Eto.Forms;
using System;
using Rhino.Collections;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Display;
using FrameLineEto.Common.Methods;
using FrameLineEto.Common.Commands;
using System.Data;
using System.Linq;

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
            var _width = 65;
            RhinoList<TextBox> main_texts = new RhinoList<TextBox>();
            RhinoList<TextBox> mod1_texts = new RhinoList<TextBox>();
            RhinoList<TextBox> mod2_texts = new RhinoList<TextBox>();
            RhinoList<TextBox> mod3_texts = new RhinoList<TextBox>();

            #region Frame line
            var fromTextBox = new TextBox
            {
                ToolTip = "Type starting frame of frame line.",
                Width = _width,
            };
            main_texts.Add(fromTextBox);
            var toTextBox = new TextBox
            {
                ToolTip = "Type end frame of frame line.",
                Width = _width,
            };
            main_texts.Add(toTextBox);
            var spacingTextBox = new TextBox
            {
                ToolTip = "Type typical spacing of frame line.",
                Width = _width,
            };
            main_texts.Add(spacingTextBox);
            #endregion

            #region Frame line modification #1
            var fromTextBox_1 = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _width,
            };
            mod1_texts.Add(fromTextBox_1);
            var toTextBox_1 = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _width,
            };
            mod1_texts.Add(toTextBox_1);
            var spacingTextBox_1 = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _width,
            };
            mod1_texts.Add(spacingTextBox_1);
            #endregion

            #region Frame line modification #2
            var fromTextBox_2 = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _width,
            };
            mod2_texts.Add(fromTextBox_2);
            var toTextBox_2 = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _width,
            };
            mod2_texts.Add(toTextBox_2);
            var spacingTextBox_2 = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _width,
            };
            mod2_texts.Add(spacingTextBox_2);
            #endregion

            #region Frame line modification #3
            var fromTextBox_3 = new TextBox
            {
                ToolTip = "Type starting frame of modification.",
                Width = _width,
            };
            mod3_texts.Add(fromTextBox_2);
            var toTextBox_3 = new TextBox
            {
                ToolTip = "Type end frame of modification.",
                Width = _width,
            };
            mod3_texts.Add(toTextBox_2);
            var spacingTextBox_3 = new TextBox
            {
                ToolTip = "Type spacing of modification.",
                Width = _width,
            };
            mod3_texts.Add(spacingTextBox_3);
            #endregion

            #region Buttons
            var clear_button = new Button { Text = "Clear" };
            clear_button.Click += (sender, e) => ClearMainParam(main_texts);
            var clear_mod_button = new Button { Text = "Clear" };
            clear_button.Click += (sender, e) => ClearMainParam(mod1_texts);

            var hello_button = new Button { Text = "Create" };
            hello_button.Click += (sender, e) => CreateFrameLine(main_texts, mod1_texts, mod2_texts, mod3_texts);

            var close_button = new Button { Text = "Cancel" };
            close_button.Click += (sender, e) => Close();
            #endregion

            #region
            var layout = new TableLayout
            {
                Spacing = new Size(5, 5),
                Padding = new Padding(10),
                
                Rows =
                {
                    new TableRow
                    {
                        Cells =
                        {
                            new Label{Text = "From", TextAlignment = TextAlignment.Left, Width = _width},
                            new Label{Text = "To", TextAlignment = TextAlignment.Left, Width = _width},
                            new Label{Text = "Spacing", TextAlignment = TextAlignment.Left, Width = _width},
                        }
                    },

                    new TableRow
                    {
                        Cells =
                        {
                            new TableCell(fromTextBox),
                            new TableCell(toTextBox),
                            new TableCell(spacingTextBox),
                        },
                    },

                    null,

                    new TableRow(null, null, clear_button),

                    null,

                    new TableRow(new Label{ Text = "Modifications"}),

                    new TableRow
                    {
                        Cells =
                        {
                            new Label{Text = "From", TextAlignment = TextAlignment.Left, Width = _width},
                            new Label{Text = "To", TextAlignment = TextAlignment.Left, Width = _width},
                            new Label{Text = "Spacing", TextAlignment = TextAlignment.Left, Width = _width},
                        }
                    },

                    new TableRow
                    {
                        Cells =
                        {
                            new TableCell(fromTextBox_1),
                            new TableCell(toTextBox_1),
                            new TableCell(spacingTextBox_1),
                        },
                    },

                    new TableRow
                    {
                        Cells =
                        {
                            new TableCell(fromTextBox_2),
                            new TableCell(toTextBox_2),
                            new TableCell(spacingTextBox_2),
                        },
                    },

                    new TableRow
                    {
                        Cells =
                        {
                            new TableCell(fromTextBox_3),
                            new TableCell(toTextBox_3),
                            new TableCell(spacingTextBox_3),
                        },
                    },

                    null,

                    new TableRow(null, null, clear_mod_button),

                    null,

                    new TableRow
                    {
                        Cells =
                        {
                            null,
                            new TableCell(hello_button),
                            new TableCell(close_button),
                        },
                    }
                },
            };
            #endregion

            Content = layout;
        }

        public void CreateFrameLine(RhinoList<TextBox> texts, RhinoList<TextBox> texts_mod1, RhinoList<TextBox> texts_mod2, RhinoList<TextBox> texts_mod3)
        {
            RhinoList<Spacing> spacings = new RhinoList<Spacing>();
            if (!CheckList(texts))
            {
                return;
            }
            Rhino.RhinoApp.WriteLine("xx");
            GetParams(texts, ref spacings);
            if (CheckList(texts_mod1)) GetParams(texts_mod1, ref spacings);
            if (CheckList(texts_mod2)) GetParams(texts_mod2, ref spacings);
            if (CheckList(texts_mod3)) GetParams(texts_mod3, ref spacings);

            // Create object of Class FrameLine with spacings as input
            FrameLine frameLine = new FrameLine(spacings);

            var doc = Rhino.RhinoDoc.ActiveDoc;
            // Backup of current layer
            var layerBackUp = doc.Layers.CurrentLayer;

            //Creating layer for frameline
            Layer flineLayer = new Layer();
            flineLayer.Name = "FRAME LINE";
            doc.Layers.Add(flineLayer);
            doc.Layers.SetCurrentLayerIndex(doc.Layers.FindName(flineLayer.Name).Index, true);

            // Creating layer for labels (child of frameline)
            Layer labelLayer = new Layer();
            labelLayer.Name = "LABELS";
            labelLayer.ParentLayerId = doc.Layers.FindIndex(doc.Layers.FindName(flineLayer.Name).Index).Id;
            doc.Layers.Add(labelLayer);

            // Grupowanie
            var groupName = "FRAME LINE GROUP";
            if (doc.Groups.Count == 0 || doc.Groups.FindName(groupName) == null)
            {
                doc.Groups.Add(groupName);
            }
            if (doc.Groups.FindName(groupName) != null)
            {
                var oldFrameLine = doc.Objects.FindByGroup(doc.Groups.FindName(groupName).Index);
                foreach (var element in oldFrameLine)
                {
                    doc.Objects.Delete(element);
                }
            }

            int frameHeight = 400; // Vertical lines height

            CreateCrossLines(doc, frameLine, groupName, frameHeight);

            doc.Layers.SetCurrentLayerIndex(doc.Layers.FindName(labelLayer.Name).Index, true);

            AddLabels(doc, frameLine, groupName, frameHeight);

            // Adding polyline to document
            doc.Layers.SetCurrentLayerIndex(doc.Layers.FindName(flineLayer.Name).Index, true);
            var polyID = doc.Objects.AddPolyline(frameLine.polyPoints);
            doc.Groups.AddToGroup(doc.Groups.FindName(groupName).Index, polyID);

            // Redrawing views
            doc.Views.Redraw();

            // Restoring previous layer and locking frameline layer
            doc.Layers.SetCurrentLayerIndex(layerBackUp.Index, true);
            doc.Layers.FindIndex(doc.Layers.FindName(labelLayer.Name).Index).IsLocked = true;
            doc.Layers.FindIndex(doc.Layers.FindName(flineLayer.Name).Index).IsLocked = true;

            return;
        }

        void GetParams(RhinoList<TextBox> texts, ref RhinoList<Spacing> spacingList)
        {
            Spacing spacing = new Spacing(int.Parse(texts[0].Text),int.Parse(texts[1].Text),int.Parse(texts[2].Text));
            spacingList.Add(spacing);
        }

        void ClearMainParam(RhinoList<TextBox> texts)
        {
            ClearTextBoxes(texts);
        }

        void ClearModParam(RhinoList<TextBox> texts)
        {
            ClearTextBoxes(texts);
        }

        void ClearTextBoxes(RhinoList<TextBox> textBoxes)
        {
            foreach (var textbox in textBoxes)
            {
                textbox.Text = "";
            }
        }

        bool CheckList(RhinoList<TextBox> texts)
        {
            foreach (var text in texts)
            {
                if(text.Text == "")
                {
                    return false;
                }
            }
            return true;
        }

        void CreateCrossLines(Rhino.RhinoDoc doc, FrameLine frameLine, string groupName, int frameHeight)
        {
            // Drawing frameline lines
            for (int i = 0; i < frameLine.polyPoints.Count; i++)
            {
                var line1ID = doc.Objects.AddLine(new Line(new Point3d(frameLine.polyPoints[i][0], frameLine.polyPoints[i][1] - frameHeight / 2, 0),
                                                new Point3d(frameLine.polyPoints[i][0], frameLine.polyPoints[i][1] + frameHeight / 2, 0)));
                var line2ID = doc.Objects.AddLine(new Line(new Point3d(frameLine.polyPoints[i][0], 0, frameLine.polyPoints[i][1] - frameHeight / 2),
                                                new Point3d(frameLine.polyPoints[i][0], 0, frameLine.polyPoints[i][1] + frameHeight / 2)));
                doc.Groups.AddToGroup(doc.Groups.FindName(groupName).Index, line1ID);
                doc.Groups.AddToGroup(doc.Groups.FindName(groupName).Index, line2ID);
            }
        }

        void AddLabels(Rhino.RhinoDoc doc, FrameLine frameLine, string groupName, int frameHeight)
        {
            int textHeight = 150; // Text height

            // Adding labels
            for (int i = 0; i < frameLine.polyPoints.Count; i++)
            {
                if (frameLine.ifLabelList[i])
                {
                    Text3d tkst = new Text3d("Fr " + frameLine.framesList[i].ToString());
                    Text3d tkstRotated = new Text3d("Fr " + frameLine.framesList[i].ToString());

                    tkst.Height = textHeight;
                    tkstRotated.Height = textHeight;

                    tkst.HorizontalAlignment = Rhino.DocObjects.TextHorizontalAlignment.Center;
                    tkst.VerticalAlignment = Rhino.DocObjects.TextVerticalAlignment.Middle;

                    tkstRotated.HorizontalAlignment = Rhino.DocObjects.TextHorizontalAlignment.Center;
                    tkstRotated.VerticalAlignment = Rhino.DocObjects.TextVerticalAlignment.Middle;

                    tkst.TextPlane = new Plane(new Point3d(frameLine.polyPoints[i][0], -frameHeight, 0), new Vector3d(0.0, 0.0, 1.0));
                    Plane rotPlane = new Plane(new Point3d(frameLine.polyPoints[i][0], 0, -frameHeight), new Vector3d(0.0, 0.0, 1.0));
                    rotPlane.Rotate(Math.PI / 2, new Vector3d(1.0, 0.0, 0.0));
                    tkstRotated.TextPlane = rotPlane;

                    var tkstID = doc.Objects.AddText(tkst);
                    var tkstRotatedID = doc.Objects.AddText(tkstRotated);
                    doc.Groups.AddToGroup(doc.Groups.FindName(groupName).Index, tkstID);
                    doc.Groups.AddToGroup(doc.Groups.FindName(groupName).Index, tkstRotatedID);
                }
            }
        }
    }
}
