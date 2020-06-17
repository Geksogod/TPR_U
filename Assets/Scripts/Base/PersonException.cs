using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonException : Exception
{
    public PersonException(string message)
        : base(message)
    { }
}
