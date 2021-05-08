# Filament.NET

**Filament.NET** is a C# Wrapper for Google's [Filament](https://github.com/google/filament) real-time rendering engine.

**Filament.NET** contains two projects: `Filament.Native` and `Filament`.

`Filament.Native` contains straight P/Invoke bindings for those people that want to do their own thing. The bindings use a C library written specifically for this wrapper, this library can be found in our fork of `Filament` [here](https://github.com/chicken-with-lips/filament/tree/main/dotnet/filament). To build this you must follow the build instructions [here](https://github.com/chicken-with-lips/filament/blob/main/BUILDING.md) or you can grab the precompiled binaries from one of our releases.

`Filament` is a layer on top with some C# niceties and attempts to conform to common C# idioms. While designing this wrapper we have tried to remain as close to the original C++ API, because of this the [documentation](https://github.com/google/filament#documentation) provided by Google should remain relevant.

## Installation
You can grab one of our releases and use our precompiled binaries or build from source. Nuget packages will follow at some point.

# Samples
A number of samples have been ported from C++ to C# to get you started. These can be found [here](https://github.com/chicken-with-lips/filament-dotnet/tree/master/Samples).

## Reporting Issues
Only **Filament.NET** specific issues should be reported, general API questions/issues should be asked on the [Filament](https://github.com/google/filament/issues) project.

## License
Please see [LICENSE](https://github.com/chicken-with-lips/filament-dotnet/blob/master/LICENSE).
