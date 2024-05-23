using Erm.BusinessLayer;
using Erm.BusinessLayer.Services;


internal class Program
{
    internal static async Task Main()
    {
        IRiskProfileService riskProfileService = new RiskProfileService();

        string cmd = string.Empty;
        while (!cmd.Equals(CommandHelper.ExitCommand))
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(CommandHelper.InputSymbol);
                cmd = Console.ReadLine();

                switch (cmd)
                {
                    case CommandHelper.CrateRiskProfileCommand:
                        Console.Write("Введите название риска : ");
                        string rpName = Console.ReadLine();

                        Console.Write("Введите описание для риска : ");
                        string rpDesccription = Console.ReadLine();

                        Console.Write("Введите связанные бизнес-процессы : ");
                        string rpBusinessProcess = Console.ReadLine();

                        Console.Write("Введите вероятность риска : ");
                        int rpOccurrenceProbability = int.Parse(Console.ReadLine());

                        Console.Write("Введите влияние риска : ");
                        int rpPotentialBusinessImpact = int.Parse(Console.ReadLine());

                        RiskProfileInfo riskProfileInfo = new(
                            rpName, rpDesccription, rpBusinessProcess,
                            rpOccurrenceProbability, rpPotentialBusinessImpact);

                        await riskProfileService.CreateAsync(riskProfileInfo);

                        break;


                    case CommandHelper.QueryRiskProfileCommand:
                        string query = Console.ReadLine();

                        IEnumerable<RiskProfileInfo> riskProfileInfos = await riskProfileService.QueryAsync(query);
                        foreach (RiskProfileInfo item in riskProfileInfos)
                        {
                            Console.WriteLine(item);
                        }

                        break;

                    case CommandHelper.GetRiskProfileCommand:
                        string name = Console.ReadLine();
                        Console.WriteLine(await riskProfileService.GetAsync(name));
                        break;

                    case CommandHelper.HelpCommand:
                        Console.WriteLine(CommandHelper.InputSymbol + CommandHelper.CrateRiskProfileCommand + " -> " +
                                          CommandHelper.CrateRiskProfileDescription);
                        Console.WriteLine(CommandHelper.InputSymbol + CommandHelper.HelpCommand + " -> " +
                                          CommandHelper.HelpDescription);
                        Console.WriteLine(CommandHelper.InputSymbol + CommandHelper.ExitCommand + " -> " +
                                          CommandHelper.ExitDescription);
                        break;

                    case CommandHelper.ExitCommand:
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(CommandHelper.UnknownCommand);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(CommandHelper.InputSymbol + ex.Message);
                throw;
            }
        }
    }
}

file static class CommandHelper
{
    public const string InputSymbol = "> ";
    public const string ExitCommand = "exit";
    public const string ExitDescription = "Exit.";
    public const string HelpCommand = "help";
    public const string HelpDescription = "Help.";
    public const string CrateRiskProfileCommand = "create_profile";
    public const string CrateRiskProfileDescription = "Create Risk Profile.";
    public const string QueryRiskProfileCommand = "search_profile";
    public const string GetRiskProfileCommand = "get_profile";
    public const string UnknownCommand = "Unknown command, use 'help' to see list of available commands";
}