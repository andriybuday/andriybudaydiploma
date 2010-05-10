package Data;

	public interface ILearningDataProvider
	{
		int getLearningVectorsCount();
		int getDataVectorDimention();
		double[] GetLearingDataVector(int vectorIndex);
	}