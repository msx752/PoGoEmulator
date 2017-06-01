[![Stories in Ready](https://badge.waffle.io/msx752/PoGoEmulator.png?label=ready&title=Ready)](https://waffle.io/msx752/PoGoEmulator?utm_source=badge) 
[![Stories in Ready](https://badge.waffle.io/msx752/PoGoEmulator.svg?label=Backlog&title=Backlog)](http://waffle.io/msx752/PoGoEmulator) 
[![Stories in Ready](https://badge.waffle.io/msx752/PoGoEmulator.svg?label=in%20progress&title=In%20Progress)](http://waffle.io/msx752/PoGoEmulator)
[![Stories in Ready](https://badge.waffle.io/msx752/PoGoEmulator.svg?label=done&title=Done)](http://waffle.io/msx752/PoGoEmulator)

[![ConsoleApp Build status](https://ci.appveyor.com/api/projects/status/jvwuq446ja59cekt/branch/master?svg=true)](https://ci.appveyor.com/project/msx752/pogoemulator/branch/master) 

# PoGoEmulator


# Installation
- install to Computer [Nox App Player](https://www.bignox.com/blog/category/releasenote/)
- install to Nox [Xposed Installer(rooted device)](http://repo.xposed.info/module/de.robv.android.xposed.installer)
- install to XposedModules [Xposed Pokemon Module(configure it with emulator ip:port)](http://repo.xposed.info/module/com.vivek.xposedpokemon)
- install to Nox [PokemonGo v0.35.0](http://www.apkmirror.com/apk/niantic-inc/pokemon-go/pokemon-go-0-35-0-release/pokemon-go-0-35-0-android-apk-download/)


# IIS Configuration
- using local ip for accesing from nox app player we have to allowed port on firewall.
- run CMD  as 'Administrator' and call `netsh http add urlacl url=http://*:3000/ user=Everyone` , it is allow to the specific ip for 3000 port.
- and also run VS as 'Administrator' and open project then configure the Project `Properties>Web>Project URL` after this


# Data Folder
- v0.35.0 [download](https://mega.nz/#!aEBGmZ7b!EwSmPmyJxcO0PYUYzuk5Suy3s8j-V99yvz0oMTtEmnI)
