using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(int memberID) => MemberDAO.Instance.Remove(memberID);

        public List<string> GetListCity() => MemberDAO.Instance.GetListCity();

        public List<string> GetListCountry() => MemberDAO.Instance.GetListCountry();

        public MemberObject GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);

        public List<MemberObject> GetMemberByFilter(string filterValue) => MemberDAO.Instance.GetMemberByFilter(filterValue);

        public MemberObject GetMemberByID(int memberID) => MemberDAO.Instance.GetMemberByID(memberID);

        public IEnumerable<MemberObject> GetMembers() => MemberDAO.Instance.GetMemberList;

        public void InsertMember(MemberObject member) => MemberDAO.Instance.AddNew(member);

        public List<MemberObject> SearchMember(string searchValue) => MemberDAO.Instance.Search(searchValue);

        public List<MemberObject> SortDescenByName() => MemberDAO.Instance.Sort();

        public void UpdateMember(MemberObject member) => MemberDAO.Instance.Update(member);


    }
}
