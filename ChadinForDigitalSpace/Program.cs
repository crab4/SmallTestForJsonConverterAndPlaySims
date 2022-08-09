
namespace ChadinForDigitalSpace {
    class Program {
        static void Main(string[] args) {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            GenerateHundredOfHundredpeople generator = new GenerateHundredOfHundredpeople();
            var list = generator.Generator(10000);

            JsCreator.CreateFileJson(list);

            List<Object> newList = JsCreator.ReadFromFileJson();


            int pCount, pCardCount, cAvgAge;
            JsCreator.GiveAnswers(out pCount, out pCardCount, out cAvgAge, newList);
            
            Console.WriteLine($"{pCount} {pCardCount} {cAvgAge}");
            

        }
    }
}