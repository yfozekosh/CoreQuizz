dotnet ef database drop --context SurveyContext --startup-project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj
dotnet ef database drop --context IdentityContext --startup-project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj --project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj
dotnet ef database update --context SurveyContext --startup-project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj
dotnet ef database update --context IdentityContext --startup-project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj --project=..\CoreQuizz.WebService\CoreQuizz.WebService.csproj
