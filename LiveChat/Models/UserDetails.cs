using System.Linq;
using Realms;

namespace LiveChat
{
    public class UserDetails : RealmObject
    {
        public static UserDetails Current
        {
            get
            {
                var config = new RealmConfiguration
                {
                    ObjectClasses = new[] { typeof(UserDetails) },
                };

                var realm = Realm.GetInstance(config);

                var current = realm.All<UserDetails>().FirstOrDefault();
                if (current == null)
                {
                    realm.Write(() =>
                    {
                        current = realm.Add(new UserDetails());
                    });
                }

                return current;
            }
        }

        public string Username { get; set; }

        public string ServerUrl { get; set; }
    }
}
