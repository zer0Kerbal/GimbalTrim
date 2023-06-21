#region GPL-2.0+ARR
/*
 * GimbalTrim (TRIM) for Kerbal Space Program
 * 
 * Copyright (c) 2014 Sébastien GAGGINI AKA Sarbian, France
 * Copyright (c) 2020, 2023 zer0Kerbal
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or (at
 * your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA.
*/
#endregion

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("GimbalTrim (TRIM)")]
[assembly: AssemblyDescription("For when you don't really want to go that way. This is a library plugin that adds a trim feature to the stock gimbal. for Kerbal Space Program.")]
[assembly: AssemblyCompany("zer0Kerbal")]
[assembly: AssemblyProduct("GimbalTrim")]
[assembly: AssemblyCopyright("Copyright © Sarbian 2016, zer0Kerbal 2020, 2023")]
[assembly: AssemblyTrademark("Trademark ™ Sarbian 2016, zer0Kerbal 2020, 2023")]
[assembly: AssemblyCulture("Neutral")]
[assembly: ComVisible(false)]

#if DEBUG 
  [assembly: AssemblyConfiguration("Debug")]
#else
   [assembly: AssemblyConfiguration("Release")]
#endif

[assembly: Guid("EC9F45E9-2C5F-4503-9D1C-7E5CBE128F12")]