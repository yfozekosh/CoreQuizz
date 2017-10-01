rm -rf .\Migrations
rm -rf ..\CoreQuizz.WebService\Migrations
dotnet ef migrations add %1 --context SurveyContext --startup-project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj
dotnet ef migrations add %1 --context IdentityContext --startup-project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj --project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj
