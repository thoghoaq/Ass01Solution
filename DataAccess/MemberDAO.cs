using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static List<MemberObject> MemberList = new List<MemberObject>()
        {
            new MemberObject{ MemberID=1, MemberName="Ngo Thi Lan Huong",
                Email="lanngo123@gmail.com", Password="12345678", City="Bien Hoa", Country="Dong Nai"},
            new MemberObject{ MemberID=2, MemberName="Thai Thi My Dung",
                Email="dungthaitcutephomai@gmail.com", Password="dungdethuong123", City="Buon Ma Thuot", Country="Dak Lak"},
        };
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<MemberObject> GetMemberList => MemberList;
        public MemberObject GetMemberByID(int memberID)
        {
            MemberObject member = MemberList.SingleOrDefault(pro => pro.MemberID == memberID);
            return member;
        }

        public MemberObject GetMemberByEmail(string email)
        {
            MemberObject member = MemberList.SingleOrDefault(pro => pro.Email == email);
            return member;
        }

        public void AddNew(MemberObject member)
        {
            MemberObject pro = GetMemberByID(member.MemberID);
            if (pro == null)
            {
                MemberList.Add(member);
            } else
            {
                throw new Exception("Member is already exists");
            }
        }

        public void Update(MemberObject member)
        {
            MemberObject m = GetMemberByID(member.MemberID);
            if (m != null)
            {
                var index = MemberList.IndexOf(m);
                MemberList[index] = member;
            } else
            {
                throw new Exception("Member does not already exists");
            }

        }
        public void Remove(int memberID)
        {
            MemberObject mem = GetMemberByID(memberID);
            if (mem != null)
            {
                MemberList.Remove(mem);
            } else
            {
                throw new Exception("Member does not already exists");
            }
        }

        public List<MemberObject> Search (string searchValue)
        {
            List<MemberObject> list = MemberList;
            var listAfterSearch = new List<MemberObject>();
            if (int.TryParse(searchValue, out int value))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberID == int.Parse(searchValue))
                    {
                        listAfterSearch.Add(list[i]);
                    }
                }
            } else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberName.Contains(searchValue))
                    {
                        listAfterSearch.Add(list[i]);
                    }
                }
            }
            return listAfterSearch;
        }

        public List<string> GetListCity()
        {
            List<string> list = new List<string>();
            string firstChild = MemberList[0].City;
            list.Add(firstChild);
            for(int i=0;i< MemberList.Count;i++)
            {
                if(!MemberList[i].City.Equals(firstChild))
                {
                    list.Add(MemberList[i].City);
                }
            }
            return list;
        }

        public List<string> GetListCountry()
        {
            List<string> list = new List<string>();
            string firstChild = MemberList[0].Country;
            list.Add(firstChild);
            for (int i = 0; i < MemberList.Count; i++)
            {
                if (!MemberList[i].Country.Equals(firstChild))
                {
                    list.Add(MemberList[i].Country);
                }
            }
            return list;
        }

        public List<MemberObject> GetMemberByFilter(string filterValue)
        {
            List<MemberObject> list = MemberList;
            var listAfterFilter = new List<MemberObject>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].City.Equals(filterValue) || list[i].Country.Equals(filterValue))
                {
                    listAfterFilter.Add(list[i]);
                }
            }
            return listAfterFilter;
        }

        public List<MemberObject> Sort()
        {
            List<MemberObject> listAfterSort = new List<MemberObject>();
            var list = MemberList;
            for(int i = 0; i < list.Count - 1 ; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    MemberObject temp;
                    if (list[i].MemberName[0] < list[j + 1].MemberName[0])
                    {
                        temp = list[i];
                        list[i] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }            }
            listAfterSort = list;
            return listAfterSort;
        }
    }
}
