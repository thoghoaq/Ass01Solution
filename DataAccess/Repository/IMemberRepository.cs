using BusinessObject;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMemberByID(int memberID);
        MemberObject GetMemberByEmail(string email);
        void InsertMember(MemberObject member);
        void DeleteMember(int memberID);
        void UpdateMember(MemberObject member);
        List<MemberObject> SearchMember(string searchValue);
        List<MemberObject> GetMemberByFilter(string filterValue);
        List<string> GetListCity();
        List<string> GetListCountry();
        List<MemberObject> SortDescenByName();

    }
}
