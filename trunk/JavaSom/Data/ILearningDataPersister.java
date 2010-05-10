package Data;

	public interface ILearningDataPersister
	{
		java.util.ArrayList<java.util.ArrayList<Double>> GetData();

		void SaveData(java.util.ArrayList<java.util.ArrayList<Double>> data);
	}