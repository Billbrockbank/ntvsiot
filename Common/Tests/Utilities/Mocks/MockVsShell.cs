﻿//*********************************************************//
//    Copyright (c) Microsoft. All rights reserved.
//    
//    Apache 2.0 License
//    
//    You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//    
//    Unless required by applicable law or agreed to in writing, software 
//    distributed under the License is distributed on an "AS IS" BASIS, 
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
//    implied. See the License for the specific language governing 
//    permissions and limitations under the License.
//
//*********************************************************//

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace TestUtilities.Mocks {
    public class MockVsShell : IVsShell {
        public readonly Dictionary<int, object> Properties = new Dictionary<int, object>();
        public readonly object ReadOnlyPropertyValue = new object();
        
        public int GetProperty(int propid, out object pvar) {
            if (Properties.TryGetValue(propid, out pvar)) {
                Console.WriteLine("MockVsShell.GetProperty(propid={0}) -> {1}", propid, pvar);
                return VSConstants.S_OK;
            }
            Console.WriteLine("MockVsShell.GetProperty(propid={0}) -> <unknown value>", propid);
            return VSConstants.E_INVALIDARG;
        }

        public int SetProperty(int propid, object var) {
            object value;
            if (Properties.TryGetValue(propid, out value)) {
                if (value == ReadOnlyPropertyValue) {
                    Console.WriteLine("MockVsShell.SetProperty(propid={0}, var={1}) -> E_INVALIDARG", propid, var);
                    return VSConstants.E_INVALIDARG;
                }
                Console.WriteLine("MockVsShell.SetProperty(propid={0}, var={1}) replacing {2}", propid, var, value);
            } else {
                Console.WriteLine("MockVsShell.SetProperty(propid={0}, var={1})", propid, var);
            }
            Properties[propid] = var;
            return VSConstants.S_OK;
        }


        public int AdviseBroadcastMessages(IVsBroadcastMessageEvents pSink, out uint pdwCookie) {
            throw new NotImplementedException();
        }

        public int AdviseShellPropertyChanges(IVsShellPropertyEvents pSink, out uint pdwCookie) {
            throw new NotImplementedException();
        }

        public int GetPackageEnum(out IEnumPackages ppenum) {
            throw new NotImplementedException();
        }

        public int IsPackageInstalled(ref Guid guidPackage, out int pfInstalled) {
            throw new NotImplementedException();
        }

        public int IsPackageLoaded(ref Guid guidPackage, out IVsPackage ppPackage) {
            throw new NotImplementedException();
        }

        public int LoadPackage(ref Guid guidPackage, out IVsPackage ppPackage) {
            throw new NotImplementedException();
        }

        public int LoadPackageString(ref Guid guidPackage, uint resid, out string pbstrOut) {
            throw new NotImplementedException();
        }

        public int LoadUILibrary(ref Guid guidPackage, uint dwExFlags, out uint phinstOut) {
            throw new NotImplementedException();
        }

        public int UnadviseBroadcastMessages(uint dwCookie) {
            throw new NotImplementedException();
        }

        public int UnadviseShellPropertyChanges(uint dwCookie) {
            throw new NotImplementedException();
        }
    }
}
