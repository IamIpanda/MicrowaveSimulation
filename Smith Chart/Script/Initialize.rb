# Initialize.rb

In = Smith_Chart::Help::In.Instance
Out = Smith_Chart::Help::Out.Instance
Draw = Smith_Chart::Help::Draw
Core = Smith_Chart::Help::Core
Point = Smith_Chart::Model::Point
Circle = Smith_Chart::Model::Circle

class << In
	def method_missing(nam, *args, &block)
		name = nam.to_s
		set = name[-1] == '='
		name = name[0, name.length - 1] if set
		if set
			In[name] = args[0]
		else
			return In[name]
		end
	end
end

class << Out
	def method_missing(nam, *args, &block)
		name = nam.to_s
		set = name[-1] == '='
		name = name[0, name.length - 1] if set
		if set
			In[name] = args[0]
		else
			return In[name]
		end
	end
end

# require fix
alias origin_require require
@@required_files = []
def require(str)
	return false if @@required_files.include? str
	@@required_files.push str
	str = File.join System::Windows::Forms::Application.StartupPath, str
	origin_require str.encode("gb2312")
end
