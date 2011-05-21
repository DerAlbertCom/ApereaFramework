
1.
Change your Global.asax.cs Application_Start to

        protected void Application_Start()
        {
            ApplicationStart.Initialize();
        }
		 
2. remove the "normal" MVC Initialization (you find the new intialization in the ApereaStart.Initialize)
