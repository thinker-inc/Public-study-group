# This file should contain all the record creation needed to seed the database with its default values.
# The data can then be loaded with the bin/rails db:seed command (or created alongside the database with db:setup).
#
# Examples:
#
#   movies = Movie.create([{ name: "Star Wars" }, { name: "Lord of the Rings" }])
#   Character.create(name: "Luke", movie: movies.first)

20.times do |n|
    rdate = Time.now + n.days
    Entry.create!(
        reserved_date: rdate,
        user_name: "ABC#{n}",
        user_email: "abc#{n}@sample.com",
        usage_time: 2,
        room_id: 1,
        people: 5
    )
end
puts "20件データ作成完了"
