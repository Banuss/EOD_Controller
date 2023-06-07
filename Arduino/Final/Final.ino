// Old varaibles for connumication
byte buf[2];
int newData;

long current_millis = 0;
long prev_millis = 0;
int counter = 0;

#include "motors.h"
#include "currentSensors.h"

#include <Wire.h>
#include <Adafruit_INA219.h>


// Defines
// Motor A
#define ENA 32
#define IN1 33
#define IN2 25
//Motor B
#define IN3 26
#define IN4 27
#define ENB 14

// Encoders
#define SCL 12
#define SDA 13

// Current sensors
#define CS1 34
#define CS2 35

// Variables

// Others

void setup() {
  Serial.begin(115200);       
  
  // Old code for communication
  // ina219.begin();
  // uint32_t currentFrequency;

  // Set pins
  pinMode(ENA, OUTPUT);
	pinMode(ENB, OUTPUT);
	pinMode(IN1, OUTPUT);
	pinMode(IN2, OUTPUT);
	pinMode(IN3, OUTPUT);
	pinMode(IN4, OUTPUT);
	
	// Turn off motors - Initial state
	digitalWrite(IN1, LOW);
	digitalWrite(IN2, LOW);
	digitalWrite(IN3, LOW);
	digitalWrite(IN4, LOW);
  
  setSpeedMotor1(255);
  setSpeedMotor2(255);

  uint32_t currentFrequency;
  if (! ina219.begin()) {
    Serial.println("Failed to find INA219 chip");
    while (1) { delay(10); }
  }
  delay(100);
}

void loop() {
  // Communication SEND DATA
  // current_millis = millis();
  // if (current_millis != prev_millis) {
  //   counter ++;
  //   if (counter > 100) counter = 0;{
  //     if (Serial.available() != 0) {
  //       float current_mA = 0;
  //       current_mA = ina219.getCurrent_mA();
  //       //Serial.println(current_mA);
  //       prev_millis = current_millis;
  //     }
  //   }
  // }

  // Communication GET DATA
  getData();
  if (newData == true) {
    if ((int)buf[0] == 0)
    {
      Serial.println("Bit 1 is 0");
    }
    if ((int)buf[0]== 1)
    {
      Serial.println("Bit 1 is 1");
    }
    if ((int)buf[1] == 0)
    {
      Serial.println("Bit 2 is 0");
    }
    if ((int)buf[1] == 1)
    {
      Serial.println("Bit 2 is 1");
    }
    newData = false;
  }

  // Motor setings
  Serial.println("on");
  setDirectionMotor1(0);
  setDirectionMotor2(0);
  delay(2000);
  Serial.println("switch");
  setDirectionMotor1(0);
  setDirectionMotor2(0);
  delay(2000);
  getCurrents();
}

void getData() {
  if (Serial.available() != 0) {
    Serial.readBytes(buf, 3);
    Serial.println("here");
    Serial.println((int)buf[0]);
    newData = true;
  }
}

// Old code in loop for communication

  //
  // 


