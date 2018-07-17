$scenesDirectory = 'resources/scenes/';
$destination = 'TicketTrader.Web/wwwroot/scenes';

$filesToCopy = $(Get-ChildItem $scenesDirectory -Filter *indexed.svg -Recurse | % { $_.FullName });

Copy-Item $filesToCopy $destination

echo 'Completed'
