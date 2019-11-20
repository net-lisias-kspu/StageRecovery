Development Environment Requirements
=====================================
To contribute to StageRecovery or to compile it yourself, you require the
following:

* [Visual Studio][vs_link] ([2017 Community Edition][vs2017ce_link] recommended,
  although newer versions may work as well)
* Kerbal Space Program installed with:
  * [ClickThroughBlocker][CTB_mod_link] mod installed
  * [ToolBarControl][TBC_mod_link] mod installed

Additionally, in order for the automated deployment scripts to work, you
require:
* [7-zip][7zip_link]
* [JQ][JQ_link]

Source Code Control
-------------------
The project is managed through [Git][git_link] on [GitHub][sr_gh_link].

After making (and testing) your changes, please create Pull Requests for your
changes.

Other development environments
------------------------------
Currently, contributing via a Linux or MacOS development environment is not
supported, although it should not be difficult to adapt the build environment
accordingly. Please note that the deployment scripts and automated versioning
will not work on those platforms and must be disabled.

Development Environment Configuration
=====================================
The development environment requires specifying where your KSP install is. It
is *highly recommended* to use a separate development install instead of your
main playthrough install.

KSP Install Location
--------------------
You can specify your KSP install location in 2 ways:
1. Set the **KSPDIR** environment variable.
2. Copy the **StageRecovery.Common.props.template** file to
"StageRecovery.Common.props" and edit it accordingly.

[sr_gh_link]: https://github.com/linuxgurugamer/StageRecovery

[vs2017_ce_link]: https://visualstudio.microsoft.com/vs/older-downloads/
[git_link]: https://git-scm.com/
[7zip_link]: https://www.7-zip.org/
[JQ_link]: https://stedolan.github.io/jq/download/

[CTB_mod_link]: https://github.com/linuxgurugamer/ClickThroughBlocker/releases
[TBC_mod_link]: https://github.com/linuxgurugamer/ToolbarControl/releases
