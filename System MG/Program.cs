using System;

class MG1Simulation
{
    static Random random = new Random();

    static double InterarrivalInterval(double mean)
    {
        double u = random.NextDouble(); // Losowanie liczby z przedziału [0, 1)
        return -mean * Math.Log(1 - u); // Obliczenie interwału
    }

    static double ServiceTimeA()
    {
        return 60; 
    }

    static double ServiceTimeB()
    {
        return random.NextDouble() * 120; // Losowanie czasu obsługi z zakresu [0, 120)
    }
    static double ServiceTimeC()
    {
       double u = random.NextDouble(); // Losowanie liczby z przedziału [0, 1)
       return -60 * Math.Log(1 - u); // Obliczenie zmiennej losowej z rozkładu wykładniczego
    }

    static double ServiceTimeD()
    {
        double u1 = 1 - random.NextDouble(); // Losowanie liczby z przedziału (0,1] dla uniknięcia log(0)
        double u2 = 1 - random.NextDouble();
        double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2); // Generowanie zmiennej z rozkładu normalnego
        return 60 + 20 * z; // Dopasowanie do zadanego rozkładu normalnego
    }



    static double SimulateMG1(double interarrivalMean, int numOfJobs)
    {
        double totalWaitingTime = 0;
        double prevJobEndTime = 0;
        double previousTime = 0;

        for (int i = 0; i < numOfJobs; i++)
        {
            double interarrivalTime = InterarrivalInterval(interarrivalMean); // Interwał przyjazdu pojazdu
            double serviceTime = ServiceTimeA(); // Czas obsługi zgłoszenia
          
            double serviceStartTime = Math.Max(previousTime + interarrivalTime, prevJobEndTime);
 
            totalWaitingTime += serviceStartTime - (previousTime + interarrivalTime); // Sumowanie czasów oczekiwania

            previousTime += interarrivalTime;
            prevJobEndTime = serviceStartTime + serviceTime;
        }

        return totalWaitingTime / numOfJobs; // Średni czas oczekiwania
    }

    static void Main()
    {
        double interarrivalMean = 120; // Średni czas między przyjazdami
        int numOfJobs = 1000000; // Liczba zgłoszeń do symulacji

        double averageWaitingTime = SimulateMG1(interarrivalMean, numOfJobs);
        Console.WriteLine($"Średni czas oczekiwania w buforze: {averageWaitingTime} sekund");
    }
}
