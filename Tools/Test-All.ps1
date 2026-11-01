param([string]$RimWorldRoot)

# Fails when a conditional load folder in LoadFolders.xml is missing or holds only empty XML.
$repo = Split-Path -Parent $PSScriptRoot
$loadFolders = New-Object xml
$loadFolders.Load((Join-Path $repo 'LoadFolders.xml'))

$failed = New-Object Collections.Generic.List[string]
$paths = $loadFolders.SelectNodes('/loadFolders/*/li') | ForEach-Object { $_.InnerText.Trim() } | Where-Object { $_ -like 'Common/Mods and Shit/*' } | Sort-Object -Unique
foreach ($path in $paths) {
  $full = Join-Path $repo $path
  if (-not (Test-Path -LiteralPath $full)) {
    $failed.Add("$path (missing)")
    continue
  }

  $hasContent = $false
  foreach ($file in Get-ChildItem -LiteralPath $full -Recurse -File) {
    if ($file.Extension -ne '.xml') {
      $hasContent = $true
      break
    }

    $xml = New-Object xml
    $xml.Load($file.FullName)
    if ($xml.DocumentElement.SelectNodes('*').Count -gt 0) {
      $hasContent = $true
      break
    }
  }

  if (-not $hasContent) {
    $failed.Add("$path (empty)")
  }
}

if ($failed.Count -gt 0) {
  Write-Host 'Load folders with no content:'
  $failed | ForEach-Object { Write-Host "  $_" }
  exit 1
}

Write-Host "Load folders checked: $(@($paths).Count)"
exit 0
