class final1
{
    static public void Main(string[] args)
    {
        int allTown = int.Parse(Console.ReadLine());
        string[] nameTown = new string[allTown];
        int[][] townList = new int[allTown][];
        int[] infect = new int[allTown];
        
        for (int i = 0; i < allTown; i++)
        {
            nameTown[i] = Console.ReadLine();
            int contractTown = int.Parse(Console.ReadLine());
            townList[i] = new int[contractTown];
            int counter = 0;
            while (contractTown > 0)
            {
                int tmp = int.Parse(Console.ReadLine());

                if (tmp != i && tmp < allTown)
                {
                    townList[i][counter] = tmp;
                    counter++;
                    contractTown--;
                }
                else
                {
                    Console.WriteLine("Invalid ID");
                }
            }
            Console.WriteLine("finish");
        }
        Status(allTown, nameTown, infect);

        while(true)
        {
            string inputCommand = Console.ReadLine();

            if(inputCommand == "Outbreak" ||inputCommand == "Vaccinate" ||
            inputCommand == "Lockdown")
            {
                int targetTown = int.Parse(Console.ReadLine());
                switch (inputCommand)
                {
                    case "Outbreak":
                        Outbreak(townList,targetTown,ref infect);
                        Status(allTown, nameTown, infect);
                        break;
                    case "Vaccinate":
                        Vaccinate(townList,targetTown,ref infect);
                        Status(allTown, nameTown, infect);
                        break;
                    case "Lockdown":
                        Lockdown(townList,targetTown,ref infect);
                        Status(allTown, nameTown, infect);
                        break;
                    default:
                        break;
                }
            }
            else if(inputCommand == "Spread" )
            {
                Spread(townList,ref infect,allTown);
                Status(allTown, nameTown, infect);
            }
            else if(inputCommand == "Exit") break;
            else Console.WriteLine("Invalid");
        }
    }

    static public void Status(int alltown, string[] name, int[] infect)
    {
        for (int i = 0; i < alltown; i++)
        {
            Console.Write(i + " " + name[i] + " " + infect[i]);
            Console.WriteLine("");
        }
    }

    static public void Outbreak(int[][] townList,int targetTown,ref int[] infect)
    {
        infect[targetTown] += 2;
        infectBalance(ref infect,targetTown);
        for (int i = 0; i < townList[targetTown].Length; i++)
        {
            infect[townList[targetTown][i]] += 1;
            infectBalance(ref infect,townList[targetTown][i]);
        }
    }
    static public void Vaccinate(int[][] townList,int targetTown,ref int[] infect)
    {
        infect[targetTown] = 0;
        infectBalance(ref infect,targetTown);
    }

    static public void Lockdown(int[][] townList,int targetTown,ref int[] infect)
    {
        infect[targetTown] -= 1;
        infectBalance(ref infect,targetTown);
        for (int i = 0; i < townList[targetTown].Length; i++)
        {
            infect[townList[targetTown][i]] -= 1;
            infectBalance(ref infect,townList[targetTown][i]);
        }
    }

    static public void Spread(int[][] townList,ref int[] infect,int allTown)
    {
        int[] spreadStore = new int[allTown];
        for (int i = 0; i < allTown; i++)
        {
            for (int x = 0; x < townList[i].Length; x++)
            {
                if(infect[i] < infect[townList[i][x]])
                {
                    spreadStore[i] = 1;
                }
            }
        }

        for (int i = 0; i < spreadStore.Length; i++)
        {
            if(spreadStore[i]==1) infect[i]++;
        }
    }

    static public void infectBalance(ref int[] infect, int targetTown)
    {
        if(infect[targetTown] > 3) infect[targetTown] = 3;
        else if(infect[targetTown] < 0) infect[targetTown] = 0;
    }
}
