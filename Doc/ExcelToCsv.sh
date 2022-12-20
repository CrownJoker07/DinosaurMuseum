source ~/.bash_profile
java -jar ExcelToCsv.jar excel/ excel_output/csv/ attributeType.config 2>err-ExcelToCsv.log

cp excel_output/csv/GlobalConfig.csv ../Game/Assets/GameMain/Resources/Configs/
# pause
