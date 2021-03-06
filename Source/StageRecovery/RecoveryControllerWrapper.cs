﻿/*
    This file is part of Stage Recovery /L
    © 2020 LisiasT
    © 2014-2018 magico13

    Stage Recovery /L licensed as follows:

    * GPL 3.0 : https://www.gnu.org/licenses/gpl-3.0.txt

    And you are allowed to choose the License that better suit your needs.

    Stage Recovery /L is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

    You should have received a copy of the GNU General Public License 3.0
    along with Stage Recovery /L. If not, see <https://www.gnu.org/licenses/>.
*/
/*
 * Contains code licensed under the MIT 
 * © 2018-2020 LinuxGuruGamer
*/
using System;
using System.Linq;
using System.Reflection;

using UnityEngine;

//
// RecoveryControllerWrapper
//
// usage
//  Copy the RecoveryControllerWrapper.cs file into your project.
//  Edit the namespace in RecoveryControllerWrapper.cs to match your plugin's namespace.
//  Use the RecoveryControllerWrapper Plugin's API
//  You can use RecoveryControllerWrapper.RecoveryControllerAvailable to check if the RecoveryControllerWrapper Plugin is actually available. 
//  Note that you must not call any other RecoveryControllerWrapper Plugin API methods if this returns false.
//
// Functions available
//
//      string RecoveryControllerWrapper.RecoveryControllerAvailable()
//      string RecoveryControllerWrapper.RegisterMod(string modName)
//      string RecoveryControllerWrapper.UnRegisterMod(string modName)
//      string RecoveryControllerWrapper.ControllingMod(Vessel v)
//
//  To use:
//
//      Your mod must register itself first, passing in your modname.  This MUST be done before going into either the editor or flight
//      You can unregister you rmod if you like
//      Call the ControllingMod with the vessel to find out which mod is registered to have control of it.

// TODO: Change to your plugin's namespace here.
namespace StageRecovery
{

    /**********************************************************\
    *          --- DO NOT EDIT BELOW THIS COMMENT ---          *
    *                                                          *
    * This file contains classes and interfaces to use the     *
    * Toolbar Plugin without creating a hard dependency on it. *
    *                                                          *
    * There is nothing in this file that needs to be edited    *
    * by hand.                                                 *
    *                                                          *
    *          --- DO NOT EDIT BELOW THIS COMMENT ---          *
    \**********************************************************/
    

    class RecoveryControllerWrapper
    {
        private static bool? recoveryControllerAvailable;
        private static Type calledType;

        public static bool RecoveryControllerAvailable
        {
            get
            {
                Log.info("RecoveryControllerAvailable");
                if (recoveryControllerAvailable == null)
                {
                    recoveryControllerAvailable = AssemblyLoader.loadedAssemblies.Any(a => a.assembly.GetName().Name == "RecoveryController");
                    if (recoveryControllerAvailable == true)
                    {
                        calledType = Type.GetType("RecoveryController.RecoveryController,RecoveryController");
                    }
                    else
                        Log.info("RecoveryController NOT available");
                }
                return recoveryControllerAvailable.GetValueOrDefault();
            }
        }

        static object CallRecoveryController(string func, object modName)
        {
            if (!RecoveryControllerAvailable)
            {
                return null;
            }
            Log.info("CallRecoveryController, func: {0}", func);
            try
            {
                 
                if (calledType != null)
                {
                    Log.info("calledtype not null: {0}", calledType);
                    MonoBehaviour rcRef = (MonoBehaviour)UnityEngine.Object.FindObjectOfType(calledType); //assumes only one instance of class Historian exists as this command returns first instance found, also must inherit MonoBehavior for this command to work. Getting a reference to your Historian object another way would work also.
                    if (rcRef != null)
                    {
                        Log.info("rcRef not null");
                        MethodInfo myMethod = calledType.GetMethod(func, BindingFlags.Instance | BindingFlags.Public);

                        if (myMethod != null)
                        {
                            Log.info("myMethod not null");
                            object magicValue;
                            if (modName != null)
                            {
                                magicValue = myMethod.Invoke(rcRef, new object[] { modName });
                            }
                            else
                            {
                                magicValue = myMethod.Invoke(rcRef, null);
                            }

                            return magicValue;
                        }
                        else
                        {
                            Log.info("{0} not available in RecoveryController", func); 
                        }
                    }
                    else
                    {
                        Log.info("{0} failed", func);
                        return null;
                    }
                }
                Log.info("calledtype failed");
                return null;
            }
            catch (Exception e)
            {
                Log.info("Error calling type: {0}", e);
                return null;
            }
        }

        public static  bool RegisterModWithRecoveryController(string modName)
        {
            Log.info("RegisterModWithRecoveryController");
            var s = CallRecoveryController("RegisterMod", modName);
            if (s == null)
            {
                Log.info("RegisterMod, CallRecoveryController returned null");
                return false;
            }
            Log.info("RegisterMod returning: {0}" + ((bool)s));
            return (bool)s;
        }

        public static  bool UnRegisterMod(string modName)
        {
            var s = CallRecoveryController("UnRegisterMod", modName);
            if (s == null)
            {
                return false;
            }

            return (bool)s;
        }

        public static string ControllingMod(Vessel v)
        {
            var s = CallRecoveryController("ControllingMod", v);
            return s as string;
        }
    }
}
