﻿// (C) Copyright 2017 by Andrew Nicholas. 
//
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//
// (C) Copyright 2011 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software
// in object code form for any purpose and without fee is hereby
// granted, provided that the above copyright notice appears in
// all copies and that both that copyright notice and the limited
// warranty and restricted rights notice below appear in all
// supporting documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK,
// INC. DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL
// BE UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is
// subject to restrictions set forth in FAR 52.227-19 (Commercial
// Computer Software - Restricted Rights) and DFAR 252.227-7013(c)
// (1)(ii)(Rights in Technical Data and Computer Software), as
// applicable.
//

using System;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;

namespace ADNPlugin.Revit.FileUpgrader
{

    [Serializable()]
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        Result IExternalCommand.Execute(
            ExternalCommandData commandData, 
            ref string message, 
            ElementSet elements)
        {
            UIApplication UIApp = commandData.Application;
            UIApp.DialogBoxShowing += new EventHandler<DialogBoxShowingEventArgs>(OnDialogShowing);
            UIApp.Application.FailuresProcessing += new EventHandler<FailuresProcessingEventArgs>(OnFailuresProcessing);
            UpgraderForm form = new UpgraderForm(commandData);
            form.ShowDialog();
            UIApp.Application.FailuresProcessing -= OnFailuresProcessing;
            UIApp.DialogBoxShowing -= OnDialogShowing;      
            return Result.Succeeded;
        }
        
        private static void OnFailuresProcessing(object sender, Autodesk.Revit.DB.Events.FailuresProcessingEventArgs e)
        {
            FailuresAccessor failuresAccessor = e.GetFailuresAccessor();
            IEnumerable<FailureMessageAccessor> failureMessages = failuresAccessor.GetFailureMessages();
            foreach (FailureMessageAccessor failureMessage in failureMessages) {
                if (failureMessage.GetSeverity() == FailureSeverity.Warning) {
                    failuresAccessor.DeleteWarning(failureMessage);
                }
            }
            e.SetProcessingResult(FailureProcessingResult.Continue);
        } 
        
        private static void OnDialogShowing(object o, DialogBoxShowingEventArgs e)
        {
            if (e.Cancellable) {
                e.Cancel();
            }
            //worry about this later - 1002 = cancel
            if (e.DialogId == "TaskDialog_Unresolved_References") {
                e.OverrideResult(1002);
            }
            //Don't sync newly created files. 1003 = close
            if (e.DialogId == "TaskDialog_Local_Changes_Not_Synchronized_With_Central") {
                e.OverrideResult(1003);
            }
            if (e.DialogId == "TaskDialog_Save_Changes_To_Local_File") {
                //Relinquish unmodified elements and worksets
                e.OverrideResult(1001);
            }
        }
    }
}
