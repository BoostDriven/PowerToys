﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Windows;
using FancyZonesEditor.Models;
using MahApps.Metro.Controls;

namespace FancyZonesEditor
{
    public class EditorWindow : MetroWindow
    {
        protected void OnSaveApplyTemplate(object sender, RoutedEventArgs e)
        {
            var mainEditor = App.Overlay;
            if (mainEditor.CurrentDataContext is LayoutModel model)
            {
                // If new custom Canvas layout is created (i.e. edited Blank layout),
                // it's type needs to be updated
                if (model.Type == LayoutType.Blank)
                {
                    model.Type = LayoutType.Custom;
                }

                model.Persist();
            }

            LayoutModel.SerializeDeletedCustomZoneSets();

            _backToLayoutPicker = false;
            Close();
            mainEditor.CloseEditor();
        }

        protected void OnClosed(object sender, EventArgs e)
        {
            if (_backToLayoutPicker)
            {
                App.Overlay.CloseEditor();
            }
        }

        protected void OnCancel(object sender, RoutedEventArgs e)
        {
            _backToLayoutPicker = true;
            Close();
        }

        private bool _backToLayoutPicker = true;
    }
}
