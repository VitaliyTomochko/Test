package com.tasks3.Juja;

import java.io.*;

public class EntityOutputStream implements EntityOutput {
    private final DataOutput out;

    public EntityOutputStream(OutputStream out) {
        this.out = new DataOutputStream(out);
    }

    @Override
    public void writePerson(Person person) throws IOException {
        out.writeInt(person.getAge());
        if (person.getName() == null) {
            out.writeBoolean(false);
        } else {
            out.writeBoolean(true);
            out.writeUTF(person.getName());
        }
    }

    @Override
    public void writePoint(Point point) throws IOException {
        int value = point.getX() << 4 | point.getY();
        out.writeByte(value);
    }
    public static void main(String[] args) throws IOException {

        Person expectedPerson = new Person("John", 32);

        ByteArrayOutputStream outByteArray = new ByteArrayOutputStream();
        EntityOutputStream outEntity = new EntityOutputStream(outByteArray);

        outEntity.writePerson(expectedPerson);

        byte[] rawByteArray = outByteArray.toByteArray();


        //call
        ByteArrayInputStream in = new ByteArrayInputStream(rawByteArray);
        EntityInputStream stream = new EntityInputStream(in);

        Person actualPerson = stream.readPerson();

        //check

        if (!actualPerson.getName().equals(expectedPerson.getName()))
            throw new AssertionError("Expected Person name equals " + expectedPerson.getName() + " but found " + actualPerson.getName());

        if (actualPerson.getAge() != expectedPerson.getAge())
            throw new AssertionError("Expected Person age equals " + expectedPerson.getAge() + " but found " + actualPerson.getAge());

        System.out.print("OK");
    }
}

interface EntityInput {
    public Person readPerson() throws IOException;

    public Point readPoint() throws IOException;
}

interface EntityOutput {
    public void writePerson(Person person) throws IOException;

    public void writePoint(Point point) throws IOException;
}

class Person {
    private String name;
    private int age;

    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }

    public String getName() {
        return name;
    }

    public int getAge() {
        return age;
    }

    @Override
    public String toString() {
        return "Person{name=" + name + ", age=" + age + "}";
    }
}

class Point {
    private final int x;
    private final int y;

    public Point(int x, int y) {
        if (x < 0 || 15 < x) {
            throw new IllegalArgumentException();
        }
        if (y < 0 || 15 < y) {
            throw new IllegalArgumentException();
        }
        this.x = x;
        this.y = y;
    }

    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }

    @Override
    public String toString() {
        return "Point{" + x + ", " + y + "}";
    }
}

class EntityInputStream implements EntityInput {
    private DataInput in;

    public EntityInputStream(InputStream in) throws IOException {

        this.in = new DataInputStream(in);

    }

    @Override
    public Person readPerson() throws IOException {
        int age=in.readInt();
        in.readBoolean();
        in.readBoolean();
        in.readBoolean();
        String name=in.readLine();
        System.out.println(name);


        Person person = new Person(name,age);

       return person;
    }
    @Override
    public Point readPoint() throws IOException {
        int x=in.readInt();
        int y=in.readInt();
        Point point =new Point(x,y);
        return point;
    }
}


 