dotnet build 'TicketTrader.Tools.SvgIndexer/TicketTrader.Tools.SvgIndexer.csproj'

$scenesDirectory = 'resources/scenes/';
$filesToIndex = $(Get-ChildItem $scenesDirectory -Filter *clean.svg -Recurse | % { $_.FullName })
$indexerApp = 'TicketTrader.Tools.SvgIndexer/bin/Debug/netcoreapp2.0/TicketTrader.Tools.SvgIndexer.dll'

dotnet $indexerApp $filesToIndex
