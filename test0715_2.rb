class Huang
    attr_accessor :name, :age

    def initialize(name, age)
        @name = name
        @age = age
    end

    def show
        puts "I am #{name}, at #{age}"
    end
end

class TokyoHuang < Huang
    attr_accessor :city

    def initialize(name, age, city)
      super(name, age)
      @city=city
    end

    def sencond(city)
        @city = city
    end

    def work
        puts "#{name} at #{age} work in #{city}"
    end
end

th = TokyoHuang.new('Huang', 27, 'tokyo')
th.show
th.work
