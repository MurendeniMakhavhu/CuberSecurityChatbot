using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;

namespace CyberBot
{
    internal class Program
    {
        class CyberSecurityBot
        {
            static Dictionary<string, string> userMemory = new Dictionary<string, string>();
            static Random rand = new Random();

            static void Main()
            {
                PlayVoiceGreeting();
                DisplayAsciiLogo();

                Console.Write("Enter your Name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name)) name = "User";
                userMemory["name"] = name;

                DisplayWelcomeMessage(name);

                StartChatBot(name);
            }

            static void PlayVoiceGreeting()
            {
                try
                {
                    SoundPlayer player = new SoundPlayer(@"C:\Users\RC_Student_lab\Desktop\ChatBot1\greeting.wav");
                    player.PlaySync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error playing audio: " + ex.Message);
                }
            }

            static void TypeText(string text, int delay = 25)
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
                Console.WriteLine();
            }

            static void PrintDivider()
            {
                Console.WriteLine("\n" + new string('=', 50) + "\n");
            }

            static void DisplayResponse(string message, string speaker, ConsoleColor color = ConsoleColor.Cyan)
            {
                Console.ForegroundColor = color;
                Console.Write($"{speaker}: ");
                TypeText(message);
                Console.ResetColor();
            }

            // Display ASCII art logo
            static void DisplayAsciiLogo()
            {
                Console.WriteLine(@"  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%@@@@
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%@@@
%%%%%%%%%%%%%%%%%%%%%%%%%%#%+*%%%%%%%%%%%%%%%%%%%%@%%%%%@@
%%%%%%%%%%%%%%%%%%%###%%######%%%%%%%#%%%%%%%%%%%%%%@%%%%@
%%%%%%%%%%##%%#%%%%%######*=-==*%%%%%%%%%%%%%%#*%%%%@@%@@@
%%%%%%%%%%##%%####%##*==--=*##*+++++*#%%%#%%%%%#%%%%%@%%%@
%%%%%%%%%%%#*------=-=+*####%%%%%%#**+*****++##%%%%%%%%%%@
%%%%%%%%%%###=+**#####*#%#%#%%##%%%%%%%%%%%#*%%%%%%%%%%%%@
%%%%%%%%%#%##=+#####%%%##%##%%%%%%%%%%%@@@@#*%%%%%%%%%%@@%
%%%%%%%%%%%%#=+##%%%%%###%##%%%%%%%%%%%%@%%#*#%%%%%%%%%%%@
%%%%%%%%%%%%#+*######%%#########%%%%%%%%%###*%%%#%%%%%%@%%
%%%%%%%%%%%%#+*%%##%%####**===+*##%%%#%%%@%%*%%%%#%%%%%@%%
%%%%%%%%%%%%#+*%%%%####+=+*#####*+*#%%%%%%%%#%%%%%%%%%%%%%
%%%%%%%%%%%%#+*%%%%%##*+*##+***%%#*#%%%%%%%%##%%%%%%%%%%%@
%%%%%%%%%%%%#*#%%%%%%%#+*##*=+#%%#*#%%%%%%%%#%%%%%%%%%%%@@
%%%%%%%%%%%%%*#%%%%%%##*+#%#*##%%**##%@@@@%##%%%%%%%%%%%@@
%%%%%%%%%%%%%#*#%%%%%%%%**#%%%%%*#%%%%%%%%%##%%%%%%%@%%%@@
%%%%%%%%%%%%%%##%%%%%%%%##**%%**#%%%%@@@@%###%%%%%%%%%@@%%
%%%%%%%%%%%*#%###%#%%%%%%%##*##%%%%%@@%%%#*#%%*#%%%@@%@@@%
@%%%%%%%%%%#%%%###%%@%%%%%%%%%%%%%%%@@%%###%%%#%%%%%%%%%@@
@%%%%%%%%%%%%%%%%#*%@%%%%%%%%%%%%@@%%@%*#%%%%%%%%%%%@@@@@@
@@@%%%%%%%%%%%%%%%##*%@@@@%%@@%%%%@@%*##%%%%%%%%%%%@@%@@@@
@%%@@%%%%%%%%%%%#%%%###%@%%%@@@@@@%###%%%%%%%%%%%@%%%@@@@@
@@@%%%%%%%%%%%%%%%%%%%##*%%@@@@@%###%%%%#%%%%%%%%%%@@@@@@@
@@@@%%%%@@%%%%%%%%%%%%%%##*#%%%*##%%%%%%%%%%@@@@%@@@@@@@@@
@@@@@@@@@@@@@%%%%%%%%#%%%%#%**%#%%%%%%%%@@@@@@@@@@@@@@@@@@
@@@@@@@@@@%%@@%%%%%%%%%%%%%%##%%%%%%%%@@@@%%@@@@@@@@@@@@@@
@@@@@@@@@@@@@%%%%%%%%%%%%%%%#*%%%%%%%%@%@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@%%%%%%%%%%%#%%%@%%%%%@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@%%%%@@@@@@@@@@@@@@%@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%######%#%#%##%%%%@
        ");
            }

            static void DisplayWelcomeMessage(string name)
            {
                Console.WriteLine("\n******************************");
                Console.WriteLine($"*     WELCOME, {name}!        *");
                Console.WriteLine("*                              *");
                Console.WriteLine("*   Let's stay safe online     *");
                Console.WriteLine("********************************\n");
            }

            static void StartChatBot(string name)
            {
                Console.Title = "🔒 Cybersecurity Chatbot";
                Console.ForegroundColor = ConsoleColor.Green;

                PrintDivider();
                TypeText("🔹 Welcome to the Cybersecurity Chatbot! 🔹", 40);
                PrintDivider();

                Console.WriteLine("Topics you can ask about: passwords, phishing, scams, privacy, safe browsing, data protection");
                Console.WriteLine("💬 Type 'exit' anytime to end the chat.");

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n📝 Chatbot: Ask me anything: ");
                    string userInput = Console.ReadLine();
                    Console.ResetColor();

                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        DisplayResponse("⚠️ Please say something. Try asking about cybersecurity!", "Chatbot");
                        continue;
                    }

                    if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("\n👋 Chatbot: Goodbye! Stay safe online!");
                        break;
                    }

                    PrintDivider();
                    string response = GetResponse(userInput);
                    DisplayResponse(response, "Chatbot", ConsoleColor.Cyan);
                    PrintDivider();
                }
            }

            static string GetResponse(string input)
            {
                string lowerInput = input.ToLower();
                string name = userMemory.ContainsKey("name") ? userMemory["name"] : "User";

                // Sentiment detection
                if (lowerInput.Contains("worried"))
                {
                    return "It's completely okay to feel that way. Cybersecurity can be scary, but I’m here to help you navigate it.";
                }
                else if (lowerInput.Contains("frustrated"))
                {
                    return "I'm sorry to hear you're feeling frustrated. Let’s work together to sort this out.";
                }
                else if (lowerInput.Contains("curious"))
                {
                    return "Curiosity is the first step to being cyber smart. Ask away!";
                }

                // Memory learning
                if (lowerInput.Contains("interested in"))
                {
                    string[] parts = input.Split(new string[] { "interested in" }, StringSplitOptions.None);
                    if (parts.Length > 1)
                    {
                        string interest = parts[1].Trim();
                        userMemory["interest"] = interest;
                        return $"Great! I'll remember that you're interested in {interest}. It's an important part of cybersecurity.";
                    }
                }

                // Recall interest
                if (lowerInput.Contains("remind me") && userMemory.ContainsKey("interest"))
                {
                    return $"Earlier you mentioned you're interested in {userMemory["interest"]}. Would you like to explore it further?";
                }

                // Keyword recognition
                if (lowerInput.Contains("password"))
                {
                    return "Use a strong, unique password with letters, numbers, and symbols. Avoid personal info.";
                }
                else if (lowerInput.Contains("scam"))
                {
                    return "Scams often come in emails or messages asking for urgent action. Don’t click unknown links!";
                }
                else if (lowerInput.Contains("privacy"))
                {
                    return "To protect your privacy, avoid oversharing online and adjust your app settings to limit data sharing.";
                }

                // Random phishing tips
                if (lowerInput.Contains("phishing"))
                {
                    string[] phishingTips = {
                        "Be cautious of emails asking for personal info. Look for poor grammar or urgent requests.",
                        "Hover over links before clicking to see if the URL looks suspicious.",
                        "Verify the sender’s address before responding to any email.",
                        "If it sounds too good to be true, it probably is. Avoid clicking on prizes or reward claims."
                    };
                    return phishingTips[rand.Next(phishingTips.Length)];
                }

                // Generic known questions
                Dictionary<string, string> knownQuestions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    {"how are you", "I'm functioning as expected and ready to help you with cybersecurity!"},
                    {"what's your purpose", "I'm here to help you learn how to stay safe and secure online."},
                    {"what can i ask you about", "You can ask about phishing, safe browsing, scams, passwords, privacy and more."},
                    {"how can i browse safely", "Use secure (HTTPS) websites, avoid public Wi-Fi without VPN, and keep software updated."},
                    {"what should i do if i receive a suspicious email", "Do not click links. Report the email. Verify the sender."},
                    {"how can i protect my personal data", "Use strong passwords, update software, and avoid oversharing on social media."}
                };

                foreach (var pair in knownQuestions)
                {
                    if (lowerInput.Contains(pair.Key))
                        return pair.Value;
                }

                // Unrecognized input fallback
                return "🤔 I'm not sure I understand. Can you try rephrasing or ask about a cybersecurity topic like phishing, passwords, or privacy?";
            }
        }
    }
}

