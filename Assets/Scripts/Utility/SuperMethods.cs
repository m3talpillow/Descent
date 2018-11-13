using System.Collections;
using System.Collections.Generic;

/*
 * Purpose: Stuff that superclasses should have. Just so I don't have 
 *			to explain the methods repeatedly.
 * Authors: Joe Peaden
 */ 

public interface SuperMethods
{

	// initialization methods, so that both superclasses and subclasses
	// can have access to Unity event functions.
	void startInit();

	void awakeInit();

}