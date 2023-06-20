---
permalink: /ExamplePatch.html
title: GimbalTrim (GT) - Example ModularManagement (MM) Patch
description: an error page, the sequel
tags: error,page,kerbal,ksp,zer0Kerbal,zedK
---
<!-- ExamplePatch.md v1.0.0.0
Gimbal Trim (GT)
created: 20 Jun 2022
updated: 

THIS FILE: GPL-2.0 by sarbian and [zer0Kerbal](https://github.com/zer0Kerbal) -->

## [GimbalTrim (GT)][mod]

### Sample Patch

[Home](./index.md)

```markdown
<!-- ExamplePatch.md v1.0.0.0
Gimbal Trim (GT)
created: 
updated: 

THIS FILE: GPL-2.0 by sarbian and [zer0Kerbal](https://github.com/zer0Kerbal) -->

@PART[]:NEEDS[GimbalTrim]:FOR[GimbalTrim]
MODULE
{
    name = GimbalTrim
    
    gimbalTransformName = thrustTransform // name of the transform used by the gimbal module
    
    trimRange = 30   // the range allowed for the trim (if you want all the value to be the same)
    trimRangeXP = 30 // The range in the positive direction on the X axis
    trimRangeYP = 30 // The range in the positive direction on the Y axis  
    trimRangeXN = 30 // The range in the negative direction on the X axis   
    trimRangeYN = 30 // The range in the negative direction on the Y axis    
     
    useTrimResponseSpeed = false
    trimResponseSpeed = 60	

    limitToGimbalRange = false
}
```

---

THIS FILE: CC BY-ND 4.0 by [zer0Kerbal](https://github.com/zer0Kerbal)
  used with express permission from zer0Kerbal

[mod]: https://www.curseforge.com/kerbal/ksp-mods/GimbalTrim "GimbalTrim (GT)"