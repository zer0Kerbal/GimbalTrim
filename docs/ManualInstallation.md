---
permalink: /ManualInstallation.html
title: Manual Installation
description: the flat-pack Kiea instructions, written in Kerbalese, unusally present
tags: installation,directions,page,kerbal,ksp,zer0Kerbal,zedK
---
<!-- ManualInstallation.md v1.0.0.0
GimbalTrim (TRIM)
created: 20 Jun 2022
updated: 20 Jun 2023

TEMPLATE: ManualInstallation.md v1.1.9.1
created: 01 Feb 2022
updated: 26 Apr 2023

based upon work by Lisias -->
## [GimbalTrim (TRIM)][mod]

[Home](./index.md)

For when you don't really want to go that way. This is a library plugin that adds the one missing feature of the stock gimbal - a trim feature. for Kerbal Space Program.

## Installation Instructions

### Using CurseForge/OverWolf app or CKAN

You should be all good! (check for latest version on CurseForge)

### If Downloaded from CurseForge/OverWolf manual download

To install, place the `KlockheedMartian` folder inside your Kerbal Space Program's GameData folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**
  * Delete `<KSP_ROOT>/GameData/KlockheedMartian/GimbalTrim`
* Extract the package's `KlockheedMartian/` folder into your KSP's GameData folder as follows:
  * `<PACKAGE>/KlockheedMartian` --> `<KSP_ROOT>/GameData`
    * Overwrite any preexisting folder/file(s).
  * you should end up with `<KSP_ROOT>/GameData/KlockheedMartian/GimbalTrim`

### If Downloaded from SpaceDock / GitHub / other

To install, place the `GameData` folder inside your Kerbal Space Program folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**
  * Delete `<KSP_ROOT>/GameData/KlockheedMartian/GimbalTrim`
* Extract the package's `GameData` folder into your KSP's root folder as follows:
  * `<PACKAGE>/GameData` --> `<KSP_ROOT>`
    * Overwrite any preexisting folder/file(s).
  * you should end up with `<KSP_ROOT>/GameData/KlockheedMartian/GimbalTrim`

## The following file layout must be present after installation

```markdown
<KSP_ROOT>
  + [GameData]
    + [KlockheedMartian]
      + [GimbalTrim]
        + [Compatibility]
          ...
        + [Config]
          ...
        + [Localization]
          ...
        + [Plugins]
          ...
        * #.#.#.#.htm
        * Attributions.htm
        * changelog.md
        * GimbalTrim.version
        * GPL-2.0+ARR.txt
        * ManualInstallation.htm
        * readme.htm
      ...
    ...
    * [ModularManagement][MM] or [Module Manager][omm]
    * ModuleManager.ConfigCache
  * KSP.log
  ...
```

### Dependencies

* [ModularManagement][MM] or [Module Manager][omm]

[MM]: https://www.curseforge.com/kerbal/ksp-mods/ModularManagement "ModularManagement (MM)"
[omm]: https://forum.kerbalspaceprogram.com/index.php?/topic/50533-*/ "Module Manager"

THIS FILE: CC BY-ND 4.0 by [zer0Kerbal](https://github.com/zer0Kerbal)
  used with express permission from zer0Kerbal

[mod]: https://www.curseforge.com/kerbal/ksp-mods/GimbalTrim "GimbalTrim (TRIM)"