﻿namespace BlogApp.Exceptions;

public class ModelNotFoundException : Exception
{
	public ModelNotFoundException(string message)
   : base(message) { }
}
