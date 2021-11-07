using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Stage Recovery /L Unleashed")]
[assembly: AssemblyDescription("This mod allows funds to be recovered, at a reduced rate, from dropped stages so long as they have parachutes attached (not necessarily deployed).")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(StageRecovery.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(StageRecovery.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(StageRecovery.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(StageRecovery.LegalMamboJambo.Trademark)]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("8f49c3d0-63e4-42aa-bd12-5b64982a1885")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
//[assembly: AssemblyVersion("1.8.0.0")]
[assembly: AssemblyVersion (StageRecovery.Version.Number)]
[assembly: KSPAssembly ("StageRecovery", StageRecovery.Version.major, StageRecovery.Version.minor)]
[assembly: KSPAssemblyDependency("KSPe", 2, 4)]
[assembly: KSPAssemblyDependency("KSPe.UI", 2, 4)]
