public class CheckFeverUtility
{
    public static string CheckFever(string temperature, string measurementType)
    {
        float feverThreshold = measurementType == "Celsius" ? 38 : 100.4f;
        float hypoThermiaThreshold = measurementType == "Celsius" ? 35 : 95;
        string message = "";
        float fTemperature = 0f;
        if (float.TryParse(temperature, out fTemperature))
        {
            if (fTemperature >= feverThreshold)
            {
                message = "You have a fever.";
            }
            else if (fTemperature <= hypoThermiaThreshold)
            {
                message = "You have hypothermia.";
            }
            else
            {
                message = "Good, you have a normal temperature.";
            }
        }
        else
        {
            message = "You entered a wrong type of temperature! Only write numbers!";
        }
        return message;
    }
}