// defines pins numbers
int motor= 11;
int redPin= 6;
int yellowPin1= 7;
int yellowPin2= 8;
int yellowPin3= 9;
int greenPin= 10;
const int trigPin = 3;
const int echoPin = 5;
// defines variables
long duration;
int distance;
void setup() {
  pinMode(trigPin, OUTPUT); // Sets the trigPin as an Output
  pinMode(echoPin, INPUT); // Sets the echoPin as an Input
  //set the LEDs and motor to output
  pinMode(motor, OUTPUT);
  pinMode(redPin, OUTPUT);
  pinMode(yellowPin1, OUTPUT);
  pinMode(yellowPin2, OUTPUT);
  pinMode(yellowPin3, OUTPUT);
  pinMode(greenPin, OUTPUT);
  //setting up the motor to print speed to the serial monitor
  Serial.begin(9600); // Starts the serial communication
}
void loop() {
  // Clears the trigPin
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  // Sets the trigPin on HIGH state for 10 micro seconds
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);
  // Reads the echoPin, returns the sound wave travel time in microseconds
  duration = pulseIn(echoPin, HIGH);
  // Calculating the distance
  distance = duration * 0.034 / 2;
  // Prints the distance on the Serial Monitor
  Serial.print("Distance: ");
  Serial.println(distance);

  //Setting the motor speed to scale with distance
  int mappedVal = map(distance,0,100,0,255);
  analogWrite(motor, 255-mappedVal);

  //setting up the leds to light up depending on the distance variable
  if (distance > 50){
    digitalWrite(greenPin, HIGH);
  }
  else{
    digitalWrite(greenPin, LOW);
  }

  if (distance <= 35){
    digitalWrite(yellowPin3, HIGH);
  }
  else{
    digitalWrite(yellowPin3, LOW);
  }

  if (distance < 20){
    digitalWrite(yellowPin2, HIGH);
  }
  else{
    digitalWrite(yellowPin2, LOW);
  }

  if (distance < 10){
    digitalWrite(yellowPin1, HIGH);
    digitalWrite(redPin, HIGH);
    analogWrite(motor, 0);
  }

  else{
    digitalWrite(yellowPin1, LOW);
    digitalWrite(redPin, LOW);
  }






}