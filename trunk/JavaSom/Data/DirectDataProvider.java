package Data;

import sun.reflect.generics.reflectiveObjects.NotImplementedException;

public class DirectDataProvider implements ILearningDataPersister
	{
		private java.util.ArrayList<java.util.ArrayList<Double>> privateData;
		public final java.util.ArrayList<java.util.ArrayList<Double>> getData()
		{
			return privateData;
		}
		private void setData(java.util.ArrayList<java.util.ArrayList<Double>> value)
		{
			privateData = value;
		}

		public DirectDataProvider(java.util.ArrayList<java.util.ArrayList<Double>> data)
		{
			setData(data);
		}

		public final java.util.ArrayList<java.util.ArrayList<Double>> GetData()
		{
			return getData();
		}

		public final void SaveData(java.util.ArrayList<java.util.ArrayList<Double>> data)
		{
			throw new NotImplementedException();
		}
	}