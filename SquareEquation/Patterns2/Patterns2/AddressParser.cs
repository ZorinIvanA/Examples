using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns2
{
    public class AddressParser
    {
        public virtual Object Parse(String address)
        {
            return new
            {
                Street = "Ленинский пр-т",
                Home = 6
            };
        }
    }

    public class YandexParser
    {
        public Object Parse(String url, String address)
        {
            //Идёт обращение по url к Яндексу
            return new
            {
                Street = "Ленинский пр-т",
                Home = 6
            };
        }
    }

    public class YandexParserAdapter : AddressParser
    {
        YandexParser _yandex;

        public override object Parse(string address)
        {
            return _yandex.Parse("geocode.yandex.ru", address);
        }
    }

    public class GoogleParser
    {
        public Object Parse(String url, String address)
        {
            //Идёт обращение по url к Гуглю
            return new
            {
                Street = "Ленинский пр-т",
                Home = 6
            };
        }
    }

    public class GoogleParserAdapter : AddressParser
    {
        GoogleParser _google;

        public override object Parse(string address)
        {
            return _google.Parse("geocode.google.com", address);
        }
    }

    public class AddressUser
    {
        public String _location;

        AddressParser _parser;

        public AddressUser()
        {
            GetParserViaLocation();
        }

        private void GetParserViaLocation()
        {
            IDictionary<String, AddressParser>
                parsers = new Dictionary<String, AddressParser>();
            FillParsers(parsers);

            _parser = parsers[_location];
        }

        private static void FillParsers(IDictionary<String, AddressParser> parsers)
        {
            parsers.Add(new KeyValuePair<string, AddressParser>(
                "РФ", new YandexParserAdapter()));
            parsers.Add(new KeyValuePair<string, AddressParser>(
                "США", new GoogleParserAdapter()));
            parsers.Add(new KeyValuePair<string, AddressParser>(
                "Казахстан", new YandexParserAdapter()));
        }

        public void GetAddress()
        {
            _parser.Parse("Ленинский пр-т, д.6");
        }
    }
}
