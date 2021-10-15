using System;
using System.Collections;
using System.Collections.Generic;
using UserCrud.Models;

namespace UserCrudTests
{
    public class TestData : IEnumerable<object[]>
    {

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new[] 
            { 
                new User(1, "name1", "test1@mail.com", 30, "password1"),
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
