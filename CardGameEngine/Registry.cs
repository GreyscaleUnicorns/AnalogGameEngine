namespace CardGameEngine{
    /// <summary>
    /// This class provides access to
    /// all Registry Elements
    /// </summary>
    class Registry{
        private static Registry m_instance = null;
        public static Registry Instance{
            get{
                if(m_instance == null) m_instance = new Registry();
                return m_instance;
            }
        }

    }
}
