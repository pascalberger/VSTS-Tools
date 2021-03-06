param (
    [string]$comments,
    [string]$includeCommentsInLog
)

Write-Verbose "Entering: comments.ps1"
Write-Verbose "  comments: $comments"
Write-Verbose "  includeCommentsInLog: $includeCommentsInLog"

# Import the Task.Common dll that has all the cmdlets we need for Build
import-module "Microsoft.TeamFoundation.DistributedTask.Task.Common"

if ($includeCommentsInLog -eq "true")
{
  Write-Output " "
  Write-Output "Comments"
  Write-Output "--------"
  Write-Output("{0}" -f $comments)
  Write-Output " "
}