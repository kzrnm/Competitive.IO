# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.0.0-beta4] - 2025-01-26
- **Breaking change** Add `Ascii` and `Asciis` type.
### Added
- **Breaking change** Change return type of `Ascii()`.
### Removed
- **Breaking change** Remove `AsciiChars()` and `AsciiSpan()`.

## [2.6.0] - 2025-01-27
### Changed
- Fix `ConsoleReader`

## [2.5.1] - 2025-01-26
### Changed
- Fix `ConsoleReader.String`
### Removed
- - Remove .NET 9 build


## [2.5.0] - 2025-01-26
### Added
- Add `ConsoleReader.Ascii(int defaultBuf)`

## [2.4.1] - 2025-01-22
### Added
- Add `ConsoleReader.Ascii(int defaultBuf)`

## [2.4.0] - 2025-01-22
### Added
- `Utf8ConsoleWriter` implements `IBufferWriter<byte>`
### Changed
- Optimize `Utf8ConsoleWriter.Write(ReadOnlySpan<byte>)`

## [2.3.1] - 2025-01-20
### Changed
- Slim embedded code

## [2.3.0] - 2025-01-08
### Added
- Add `Utf8ConsoleWriter.Write(char v, int count)`
- Add `Utf8ConsoleWriter.Write(ReadOnlySpan<byte>)`
### Changed
- Support .NET 9.
- Fix logic
- Fix document

## [2.2.0] - 2025-01-02
### Added
- Split Utf8ConsoleWriter

## [2.1.0] - 2023-12-06
### Added
- Split Utf8ConsoleWriter

## [2.0.0] - 2023-12-06
### Added
- .NET 6 support
- Read and write into buffer span

### Changed
- Shrink codes

## [1.4.0] - 2023-09-11
### Added
- WriteGrid<T>(T[,] cols)

## [1.3.1] - 2023-02-12
### Added
- Add EditorBrowsable

## [1.3.0] - 2023-02-08
### Added
- Add WriteLineJoin<T>(T[] col).

## [1.2.0] - 2023-01-31
### Changed
- Change accessibilities of some members.

## [1.1.2] - 2023-01-30
### Added
- Add *Chars to RepeatReader

## [1.1.1] - 2023-01-30
### Added
- Add `implicit operator char[](ConsoleReader cr)`

## [1.1.0] - 2023-01-30
### Added
- Add ConsoleReader.*Chars methods

### Changed
- Slim using

### Removed
- Remove `WriteLineJoin<T>(params T[] col)`

## [1.0.1] - 2022-06-19
### Changed
- Adjust PropertyConsoleReader attribute

## [1.0.0] - 2022-06-19
### Changed
- **PropertyConsoleReader extends ConsoleReader**

## [0.12.0] - 2022-04-21
### Changed
- **Remove params from Utf8ConsoleWriter.WriteLines<T>(T[] col)**

## [0.11.0] - 2022-03-21
### Changed
- **Move Grid from RepeatReader to ConsoleReader**

## [0.10.0] - 2022-03-21
### Changed
- **Fix FillEntireNumber**
- Fix Flush
- Minify codes

## [0.9.1] - 2022-03-20
### Changed
- Utf8ConsoleWriter.Write(char[])

## [0.9.0] - 2022-03-20
### Added
- Add Utf8ConsoleWriter
### Changed
- SouceExpander 5.0.0

## [0.8.0] - 2022-03-05
### Changed
- Use Utf8Parser

## [0.7.0] - 2022-02-26
### Changed
- SouceExpander 4.1.1

## [0.6.0] - 2022-02-24
### Changed
- Compress code
### Removed
- Deprecate .NET Standard 1.3

## [0.5.4] - 2022-02-24
### Added
- Add MethodImplAttribute
### Changed
- Rename WriteLineGrid to WriteGrid

## [0.5.3] - 2022-02-23
### Added
- Update libraries
- WriteLineGrid(ITuple)

## [0.5.1] - 2021-03-07
### Added
- Add uint to reader

## [0.5.0] - 2021-02-10
### Changed
- Optimize Reader

## [0.4.0] - 2021-02-07
### Added
- WriteLine Empty Line
- Add Decimal

### Removed
- Remove SplitReader

## [0.3.0] - 2021-01-09
### Added

- Add Grid
- Add SelectArray

## [0.2.1] - 2021-01-01
### Removed

- Remove WriteLineJoin T[]

## [0.2.0] - 2020-12-31
### Changed

- Add WriteLineJoin tuple

## [0.1.3] - 2020-12-18
### Changed

- Update SourceExpander

## [0.1.2] - 2020-12-18
### Added

- Add buffer size parameter

## [0.1.1] - 2020-12-18
### Added

- GenerateDocumentationFile

## [0.1.0] - 2020-12-18
### Changed

- Minify dependencies
- Remove `Split` property and Add method this one in PropertyConsoleReader

## [0.0.7] - 2020-12-17
### Changed

- Update SourceExpander
- Minify embedded code

## [0.0.6] - 2020-12-17
### Added

- SourceLink
- Add XML documents
- Add `DebuggerBrowsable(DebuggerBrowsableState.Never)` to `PropertyConsoleReader`, `PropertyRepeatReader`, `PropertySplitReader`

### Changed

- Update SourceExpander

## [0.0.5] - 2020-12-09
### Added

- Add ULong