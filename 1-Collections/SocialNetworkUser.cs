using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private readonly IDictionary<string, ISet<TUser>> _dict;
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
            _dict = new Dictionary<string, ISet<TUser>>();
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            if (_dict.ContainsKey(group))
            {
                return _dict[group].Add(user);
            }
            else
            {
                _dict[group] = new HashSet<TUser>();
                return _dict[group].Add(user);
            }
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                var allFollowed = new List<TUser>();
                foreach(var group in _dict.Values)
                {
                    foreach(var user in group)
                    {
                        allFollowed.Add(user);
                    }
                }
                return allFollowed;
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            var ret = new HashSet<TUser>();
            if (_dict.ContainsKey(group))
            {
                return new HashSet<TUser>(_dict[group]);
            }
            else
            {
                return new HashSet<TUser>();
            }
        }
    }
}
