using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GenerateRandomDomainName
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get generic word store
            IWordStore oWordStore = new WordStore();
            oWordStore.init();

            //Get domain generator
            DomainGenerator oGenerator = new DomainGenerator();
            oGenerator.init(oWordStore);

            //Gerneate 10 domains
            for (int i = 0; i < 10; ++i)
            {
                //Get the random domain name
                string strDomain = oGenerator.getRandomDomain(10);

                //Log to console
                Console.WriteLine(strDomain);
            }
        }
    }


    /// <summary>
    /// Model to genreate a new random domain name
    /// </summary>
    class DomainGenerator
    {
 
        private IWordStore _oWordStore;
        private Random _rand;

        /// <summary>
        /// Initialize the generator model
        /// </summary>
        /// <param name="oWordStore">Word store to use when generating words.  This allows for easy mocking or using a different type of word store later.</param>
        public void init(IWordStore oWordStore)
        {
            _oWordStore = oWordStore;

            _rand = new Random();
        }

        /// <summary>
        /// Gerneate a new random domain name
        /// </summary>
        /// <param name="intDomainLength">Length of the domain in number of words</param>
        /// <returns>string containing the new domain name.</returns>
        public string getRandomDomain(int intDomainLength)
        {
            string strDomain = "http://" + _oWordStore.getRandomWord();

            for (int u = 0; u < intDomainLength - 1; ++u)
            {
                strDomain += "-" + _oWordStore.getRandomWord();
            }

            strDomain += getRandomSuffix();

            return strDomain;
        }

        /// <summary>
        /// Provides a random suffix for the domain
        /// </summary>
        /// <returns>string with a random suffix</returns>
        private string getRandomSuffix()
        {
            string[] arrSuffix = new string[] { ".net", ".com", ".edu", "uk" };
            return arrSuffix[_rand.Next(0, arrSuffix.Length)];
        }



    }


    interface IWordStore
    {
        void init();
        string getRandomWord();
    }

    /// <summary>
    /// Creates a new word store data accessor
    /// This could be replaced with a database connector or a class that calls an API to get a random word.
    /// </summary>
    class WordStore : IWordStore
    {
        private string[] _arrStringStore;
        private int _intUpperStoreBound;
        private Random _rand;

        public void init()
        {
            _arrStringStore = initializeWordStore();
            _intUpperStoreBound = _arrStringStore.Length;
            _rand = new Random();
        }

        /// <summary>
        /// Returns a random word for the word store
        /// </summary>
        /// <returns>Returns a random string</returns>
        public string getRandomWord()
        {
            int intNext = _rand.Next(0, _intUpperStoreBound);
            return _arrStringStore[intNext];
        }

        /// <summary>
        /// Populates the word store
        /// </summary>
        /// <returns></returns>
        private string[] initializeWordStore()
        {
            //String courtesy of http://hipsum.co/
            string strStringStore = "You probably haven't heard of them taxidermy jean shorts, aliqua chartreuse sustainable polaroid cillum. Irure asymmetrical messenger bag voluptate tacos deserunt veniam try-hard bitters. Tilde cliche cupidatat, yr cornhole flexitarian mixtape nulla offal narwhal hoodie. Authentic literally gentrify nihil raw denim, tofu everyday carry sartorial non hoodie. Franzen post-ironic migas whatever pickled occupy brooklyn pop-up labore, swag retro fingerstache aute. Veniam food truck nihil, sint velit pickled sriracha. Deep v chicharrones pug, gastropub pop-up green juice typewriter plaid id non health goth before they sold out messenger bag DIY. Seitan bushwick bitters, kitsch eiusmod ex fanny pack. Bicycle rights mustache farm-to-table mixtape. Selvage man braid consectetur sustainable iPhone tote bag. Tacos eiusmod lo-fi bespoke meggings franzen messenger bag, shabby chic post-ironic sint 8-bit. Pabst cronut sustainable, esse aute magna tofu ugh hammock ullamco eiusmod lo-fi chartreuse duis. Bitters nihil fanny pack normcore, fingerstache bespoke blog literally marfa sriracha bicycle rights locavore chambray affogato. Sed vice magna, schlitz raw denim narwhal pop-up meggings delectus eu quinoa.";

            //Remove non word charcters
            Regex oReg = new Regex(@"[_',\d.()-]");
            strStringStore = oReg.Replace(strStringStore, "");

            string[] aReturn = strStringStore.Split();

            return aReturn;
        }
    }

}
