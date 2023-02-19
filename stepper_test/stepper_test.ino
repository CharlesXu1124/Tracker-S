// Include the Arduino Stepper Library
#include <Stepper.h>

// #define ANALOG_IN_PIN A0

// // Floats for ADC voltage & Input voltage
// float adc_voltage = 0.0;
// float in_voltage = 0.0;

// // Floats for resistor values in divider (in ohms)
// float R1 = 30000.0;
// float R2 = 7500.0; 

// // Float for Reference Voltage
// float ref_voltage = 12.0;

// // Integer for ADC value
// int adc_value = 0;

// Number of steps per output rotation
const int stepsPerRevolution = 200;

// Create Instance of Stepper library
Stepper myStepper(stepsPerRevolution, 8, 9, 10, 11);


void setup()
{
	// set the speed at 60 rpm:
	myStepper.setSpeed(120);
	// initialize the serial port:
	Serial.begin(115200);
}

void loop() 
{

  while (Serial.available() == 0) {}

  // // Read the Analog Input
  //  adc_value = analogRead(ANALOG_IN_PIN);
   
  //  // Determine voltage at ADC input
  //  adc_voltage  = (adc_value * ref_voltage) / 1024.0; 
   
  //  // Calculate voltage at divider input
  //  in_voltage = adc_voltage / (R2/(R1+R2)) / 2 ; 
   
   // Print results to Serial Monitor to 2 decimal places
  // Serial.print("Input Voltage = ");
  // Serial.println(in_voltage, 2);

  char rotationCmd = Serial.read();

	// rotate the solar panel in one direction

  if (rotationCmd == 'a') {
	  myStepper.step(stepsPerRevolution / 20);
	  delay(10);

  }

  if (rotationCmd == 'b') {
	  myStepper.step(-stepsPerRevolution / 20);
	  delay(10);

  }
}